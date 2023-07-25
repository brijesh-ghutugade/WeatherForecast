using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;

namespace WeatherForecast.Contracts.Intefaces
{
    public interface IWeatherForecastDataStore
    {
        public Task<WeatherForecastDataModel> SaveUpdateWeatherForecastAsync(WeatherForecastDataModel weatherForecastResponse);
        public Task<IEnumerable<WeatherForecastDataModel>> GetForecastHistoryAsync();
        public Task<bool> DeleteWeatherForecastAsync(int id);

    }
}
