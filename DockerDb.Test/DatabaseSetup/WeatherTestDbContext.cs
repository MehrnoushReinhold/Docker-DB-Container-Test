using System;
using DockerDbContainer.Infrastructure.Database;
using DockerDbContainer.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Test.DatabaseSetup
{
    public class TestDbContext(DbContextOptions<WeatherDbContext> options) : WeatherDbContext(options)
    {
        public ModelBuilder ModelBuilder { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DockerDbContainer.Infrastructure.Database.Models.WeatherForecast>().HasData(
                new WeatherForecast{ Id = 1, Description = "Freezing", CreatedDate = DateTimeOffset.Now, WeatherForecastType = WeatherForecastType.Snow, TemperatureC = 0 }
            );

            base.OnModelCreating(modelBuilder);

            this.ModelBuilder = modelBuilder;
        }
    }
}
