using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DockerDbContainer.Infrastructure.Database.Models
{
    [ExcludeFromCodeCoverage]
    public class WeatherForecast
    {
        public int Id { get; set; }

        [StringLength(500)]
        public required string Description { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public required WeatherForecastType WeatherForecastType { get; set; }

        public decimal TemperatureC { get; set; }
    }
}
