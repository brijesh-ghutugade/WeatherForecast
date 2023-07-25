using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.DataStore.Repository;

namespace WeatherForecast.DataStore
{
    public class WeatheForecastDataStore : IWeatherForecastDataStore
    {
        private readonly IRepository<WeatherForecastDataModel> _repository;

        public WeatheForecastDataStore(IRepository<WeatherForecastDataModel> repository)
        {
            _repository = repository;
        }

        public async Task<bool> DeleteWeatherForecastAsync(int id)
        {
            await _repository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<WeatherForecastDataModel>> GetForecastHistoryAsync()
        {
            return await _repository.GetAll();
        }


        public async Task<WeatherForecastDataModel> SaveUpdateWeatherForecastAsync(WeatherForecastDataModel weatherForecast)
        {
            var existingForecast = await _repository.FindByCondition(wf => wf.Latitude == weatherForecast.Latitude && wf.Longitude == weatherForecast.Longitude,
                                                                     true,
                                                                     new List<string> { "TimelyData" });

            if (existingForecast != null && weatherForecast != null)
            {
                existingForecast.TimezoneAbbreviation = weatherForecast?.TimezoneAbbreviation;
                existingForecast.Timezone = weatherForecast?.Timezone;
                existingForecast.GenerationTime = weatherForecast?.GenerationTime ?? 0;
                existingForecast.Elevation = weatherForecast?.Elevation ?? 0;
                existingForecast.UtcOffsetSeconds = weatherForecast?.UtcOffsetSeconds ?? 0;

                existingForecast.TimelyData.Clear();
                foreach (var item in weatherForecast.TimelyData)
                {
                    existingForecast.TimelyData.Add(item);
                }

                return await _repository.Update(existingForecast);
            }

            return await _repository.Add(weatherForecast);
        }
    }
}
