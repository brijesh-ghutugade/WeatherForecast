using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Services.Abstractions;

namespace WeatherForecast.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }


        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromBody] WeatherForecastRequest weatherForecastRequest)
        {
            var weatherForecastData = await _weatherService.GetWeatherForecastAsync(weatherForecastRequest);
            return Ok(weatherForecastData);
        }


        [HttpDelete(Name = "Delete")]

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _weatherService.DeleteWeatherForecastAsync(id);
            return Ok(success);
        }


        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetHistory()
        {
            var weatherForecastData = await _weatherService.GetForecastHistoryAsync();
            return Ok(weatherForecastData);
        }
    }
}