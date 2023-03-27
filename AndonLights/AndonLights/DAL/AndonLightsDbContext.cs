using AndonLights.Model;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
using System.Configuration;
namespace AndonLights.DAL;


public class AndonLightsDbContext : DbContext
{

    public DbSet<Session> Sessions { get; set; }
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
        modelBuilder.Entity<Session>().ToTable("sessions");

        modelBuilder.Entity<Session>().HasKey(s => s.Id);
        modelBuilder.Entity<Session>().Property(s=>s.ErrorMessage).HasMaxLength(150);
        

    }

    public AndonLightsDbContext() { }  

    
}
