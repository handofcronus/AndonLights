using AndonLights.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
//using Microsoft.Extensions.Configuration;


namespace AndonLights.DAL;


public class AndonLightsDbContext : DbContext
{

    public DbSet<Session> Sessions { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<AndonLight> AndonLights { get; set; }
    public DbSet<MonthlyStateStats> MonthlyStateStats{ get; set; }
    public DbSet<DailyStateStats> DailyStateStats{ get; set; }
    public DbSet<Client> Clients { get; set; }
    public AndonLightsDbContext(DbContextOptions<AndonLightsDbContext> options ):base(options)
    {
       
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if(!optionsBuilder.IsConfigured)
        {
            var builder = WebApplication.CreateBuilder();
            var connHost = Environment.GetEnvironmentVariable("PGQL__HOST");
            var connUser = Environment.GetEnvironmentVariable("PGQL__USER");
            var connPsw = Environment.GetEnvironmentVariable("PGQL__PW");
            var connPort = Environment.GetEnvironmentVariable("PGQL__PORT");
            var connDBname = Environment.GetEnvironmentVariable("PGQL__DB");
            var connString = "";
            if (connHost is null || connUser is null || connPsw is null || connPort is null || connDBname is null)
            {
                connString = builder.Configuration.GetConnectionString("AndonLights");
            }
            else
            {
                NpgsqlConnectionStringBuilder connStringBuilder = new NpgsqlConnectionStringBuilder();
                connStringBuilder.Host = connHost;
                connStringBuilder.Port = Int32.Parse(connPort);
                connStringBuilder.Database = connDBname;
                connStringBuilder.Username = connUser;
                connStringBuilder.Password = connPsw;
                connString = connStringBuilder.ToString();
            }
            optionsBuilder.UseNpgsql(connString, o => o.UseNodaTime());
        }
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Session>().ToTable("Sessions");
        modelBuilder.Entity<Session>().HasKey(s => s.Id);
//modelBuilder.Entity<Session>().Property(s=>s.ErrorMessage).HasMaxLength(150);


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
        modelBuilder.Entity<AndonLight>().Property(a => a.LastErrorMessage).HasMaxLength(250);


        modelBuilder.Entity<DailyStateStats>().ToTable("DailyStateStats");
        modelBuilder.Entity<DailyStateStats>().HasKey(a => a.Id);
        
        modelBuilder.Entity<MonthlyStateStats>().ToTable("MonthlyStateStats");
        modelBuilder.Entity<MonthlyStateStats>().HasKey(a => a.Id);


        modelBuilder.Entity<Client>().HasKey(clients => clients.Id);
        modelBuilder.Entity<Client>().ToTable("Clients");

    }

    public AndonLightsDbContext() { }  

    
}
