using AndonLights.Model;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using System.Configuration;
namespace AndonLights.DAL;


public class AndonLightsDbContext : DbContext
{

    public DbSet<Session> Sessions { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<AndonLight> AndonLights { get; set; }
    public DbSet<MonthlyStateStats> MonthlyStateStats{ get; set; }
    public DbSet<DailyStateStats> dailyStateStats{ get; set; }
    public AndonLightsDbContext(DbContextOptions<AndonLightsDbContext> options ):base(options)
    {
       
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if(!optionsBuilder.IsConfigured)
        {
            var builder = WebApplication.CreateBuilder();
            optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("AndonLights"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Session>().ToTable("Sessions");
        modelBuilder.Entity<Session>().HasKey(s => s.Id);
        modelBuilder.Entity<Session>().Property(s=>s.ErrorMessage).HasMaxLength(150);


        modelBuilder.Entity<State>().ToTable("States");
        modelBuilder.Entity<State>().HasKey(s => s.ID);
        modelBuilder.Entity<State>().HasMany(s => s.ClosedSessions).WithOne();
        modelBuilder.Entity<State>().HasMany(s => s.DailyStats).WithOne();
        modelBuilder.Entity<State>().HasMany(s => s.MonthlyStats).WithOne();
        modelBuilder.Entity<State>().Property(a => a.StateColour).HasConversion<string>();



        modelBuilder.Entity<AndonLight>().ToTable("AndonLights");
        modelBuilder.Entity<AndonLight>().HasKey(a => a.Id);
        modelBuilder.Entity<AndonLight>().HasMany(a => a.States).WithOne().HasForeignKey(s => s.LightID).IsRequired();
        modelBuilder.Entity<AndonLight>().Property(a => a.CurrentState).HasConversion<string>();


        modelBuilder.Entity<DailyStateStats>().ToTable("DailyStateStats");
        modelBuilder.Entity<DailyStateStats>().HasKey(a => a.Id);
        


        modelBuilder.Entity<MonthlyStateStats>().ToTable("MonthlyStateStats");
        modelBuilder.Entity<MonthlyStateStats>().HasKey(a => a.Id);
        

    }

    public AndonLightsDbContext() { }  

    
}
