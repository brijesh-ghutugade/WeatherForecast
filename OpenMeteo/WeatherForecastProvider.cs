using OpenMeteo.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;

namespace OpenMeteo
{
    public class WeatherForecastProvider : IWeatherForecastProvider
    {
        private readonly IOpenMeteoClient _openMeteoClient;

        public WeatherForecastProvider(IOpenMeteoClient openMeteoClient)
        {
            _openMeteoClient = openMeteoClient;
        }
        public async Task<WeatherForecastResponseDto> GetWeatherForecastAsync(WeatherForecastRequest weatherForecastRequest)
        {
            var openMeteoResponse = await _openMeteoClient.GetForecastAsync(weatherForecastRequest);

            return openMeteoResponse.ToWeatherForecastResponse();
        }
    }
}
