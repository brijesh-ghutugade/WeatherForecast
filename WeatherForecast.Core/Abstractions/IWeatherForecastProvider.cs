using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;

namespace WeatherForecast.Contracts.Intefaces
{
    public interface IWeatherForecastProvider
    {
       public Task<WeatherForecastResponseDto> GetWeatherForecastAsync(WeatherForecastRequest weatherForecastRequest);
    }
}
