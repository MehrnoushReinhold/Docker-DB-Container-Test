using System;
using System.Collections.Generic;
using DockerDbContainer.Infrastructure.Database;
using DockerDbContainer.Infrastructure.Database.Models;

namespace Test.DatabaseSetup;

public class DataSeeder
{
    public static void SeedData(WeatherDbContext context)
    {
        var weatherForecasts = new List<WeatherForecast>
        {
            new WeatherForecast{ Description = "Freezing", CreatedDate = DateTimeOffset.Now, WeatherForecastType = WeatherForecastType.Snow, TemperatureC = 0 },
            new WeatherForecast { Description = "Bracing", CreatedDate = DateTimeOffset.Now, WeatherForecastType = WeatherForecastType.Snow, TemperatureC = 5 },
            new WeatherForecast { Description = "Chilly", CreatedDate = DateTimeOffset.Now, WeatherForecastType = WeatherForecastType.Rainy, TemperatureC = 10 },
        };
        context.AddRange(weatherForecasts);
        context.SaveChanges();
    }
}
