using System.Linq;
using System.Threading.Tasks;
using DockerDbContainer.Infrastructure.Database.Models;
using NUnit.Framework;
using Test.DatabaseSetup;

namespace Test.Infrastructure.Database
{
    [TestFixture]
    public class WeatherForecastDbRepositoryTests : DatabaseIntegrationTest
    {

        [Test]
        public async Task GetWeatherForecasts_ReturnsForecastsFromDatabase_WhenCalled()
        {
            // Arrange
            var weatherForecastType = WeatherForecastType.Snow;

            // Act
            var result = await DbFixture.WeatherDbRepository.GetForecastsOfType(weatherForecastType);

            // Assert
            var weatherForecasts = result.ToList();

            System.Diagnostics.Debug.WriteLine("Connectionstring: " + DbFixture.DbConnectionFactory.GetConnectionString(DbConnectionFactoryDocker.DatabaseName));

            Assert.That(weatherForecasts, Is.Not.Null);
            Assert.That(weatherForecasts.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task SaveWeatherForecast_PersistsData_WhenCalled()
        {
            // Arrange
            var description = "Freezing";
            var weatherForecastType = WeatherForecastType.Sunny;
            var temperature = 0;

            // Act
            await DbFixture.WeatherDbRepository.SaveWeatherForecast(description, weatherForecastType, temperature);

            var result = await DbFixture.WeatherDbRepository.GetForecastsOfType(WeatherForecastType.Sunny);

            // Assert
            var weatherForecasts = result.ToList();

            Assert.That(weatherForecasts.Count, Is.EqualTo(1));
        }
    }
}