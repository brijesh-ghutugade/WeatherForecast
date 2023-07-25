using WeatherForecast.Contracts.Models;

namespace WeatherForecast.Services.Abstractions
{
    public interface IWeatherService
    {
        public Task<WeatherForecastResponseDto> GetWeatherForecastAsync(WeatherForecastRequest weatherForecastRequest);

        public Task<bool> DeleteWeatherForecastAsync(int id);

        public Task<List<WeatherForecastResponseDto>> GetForecastHistoryAsync();
    }
}
