using DockerDbContainer.Infrastructure.Database.Models;

namespace DockerDbContainer.Infrastructure.Database.Interfaces;

public interface IWeatherDbRepository
{
    Task SaveWeatherForecast(string Description, WeatherForecastType type, decimal Temperature,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<WeatherForecast>> GetForecastsOfType(WeatherForecastType type,
        CancellationToken cancellationToken = default);
}