using DockerDbContainer.Infrastructure.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Docker_DB_Container.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherDbRepository _weatherDbRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherDbRepository weatherRepository)
        {
            _logger = logger;
            _weatherDbRepository = weatherRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get(DockerDbContainer.Infrastructure.Database.Models.WeatherForecastType type)
        {
            var weatherForecasts = await _weatherDbRepository.GetForecastsOfType(type);

            return weatherForecasts.Select(wf => new WeatherForecast { Date = DateOnly.FromDateTime(wf.CreatedDate.DateTime), Summary = wf.Description, TemperatureC = Convert.ToInt32(wf.TemperatureC) }).ToList();
        }


        [HttpPost(Name = "SaveWeatherForecast")]
        public async Task Save(string description, DockerDbContainer.Infrastructure.Database.Models.WeatherForecastType type, decimal temperature)
        {
            await _weatherDbRepository.SaveWeatherForecast(description, type, temperature);
        }
    }
}
