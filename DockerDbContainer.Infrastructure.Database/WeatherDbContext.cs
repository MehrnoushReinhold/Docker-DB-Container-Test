using DockerDbContainer.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerDbContainer.Infrastructure.Database
{
    public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
    {
        // Define your DbSets here
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Configure your entity mappings here
            modelBuilder.Entity<WeatherForecast>()
                .HasKey(e => e.Id);
        }
    }
}
