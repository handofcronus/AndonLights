using AndonLights.Model;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Runtime.CompilerServices;

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
            var connHost = Environment.GetEnvironmentVariable("SQL:HOST");
            var connUser = Environment.GetEnvironmentVariable("SQL:USER");
            var connPsw = Environment.GetEnvironmentVariable("SQL:PW");
            var connString = "";
            if (connHost is null || connUser is null || connPsw is null)
            {
                connString = builder.Configuration.GetConnectionString("AndonLights");
            }
            else
            {
                connString = $"Server={connHost};Database = AndonLightsDB;User Id ={connUser};Password={connPsw};MultipleActiveResultSets=true; TrustServerCertificate=true";
            }
            optionsBuilder.UseSqlServer(connString);
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
        modelBuilder.Entity<State>().HasMany(s => s.DailyStats)
            .WithOne()
            .HasForeignKey(s => s.StateId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<State>().HasMany(s => s.MonthlyStats)
            .WithOne()
            .HasForeignKey(s => s.StateId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<State>().Property(a => a.StateColour).HasConversion<string>();



        modelBuilder.Entity<AndonLight>().ToTable("AndonLights");
        modelBuilder.Entity<AndonLight>().HasKey(a => a.Id);
        modelBuilder.Entity<AndonLight>().HasMany(a => a.States)
            .WithOne()
            .HasForeignKey(s => s.LightID)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<AndonLight>().Property(a => a.CurrentState).HasConversion<string>();
        modelBuilder.Entity<AndonLight>().Property(a => a.Name).HasMaxLength(150);


        modelBuilder.Entity<DailyStateStats>().ToTable("DailyStateStats");
        modelBuilder.Entity<DailyStateStats>().HasKey(a => a.Id);
        


        modelBuilder.Entity<MonthlyStateStats>().ToTable("MonthlyStateStats");
        modelBuilder.Entity<MonthlyStateStats>().HasKey(a => a.Id);
        

    }

    public AndonLightsDbContext() { }  

    
}
