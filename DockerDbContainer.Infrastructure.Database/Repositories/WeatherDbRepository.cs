using System.Diagnostics.CodeAnalysis;
using DockerDbContainer.Infrastructure.Database.Interfaces;
using DockerDbContainer.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerDbContainer.Infrastructure.Database.Repositories
{
    [ExcludeFromCodeCoverage]
    public class WeatherDbRepository(WeatherDbContext dbContext) : IWeatherDbRepository
    {
        public Task SaveWeatherForecast(string Description, WeatherForecastType type, decimal Temperature, CancellationToken cancellationToken = default)
        {
            dbContext.WeatherForecasts.Add(
                new WeatherForecast
                {
                    Description = Description,
                    CreatedDate = DateTime.UtcNow,
                    WeatherForecastType = type,
                    TemperatureC = Temperature
                });

            return dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<WeatherForecast>> GetForecastsOfType(WeatherForecastType type, CancellationToken cancellationToken = default)
        {
            return await dbContext.WeatherForecasts.Where(x => x.WeatherForecastType == type).ToListAsync(cancellationToken);
        }
    }
}
