using WeatherForecast.Contracts.Models;

namespace OpenMeteo.Abstractions
{
    public interface IOpenMeteoClient
    {
        public Task<WeatherForecastProviderResponse> GetForecastAsync(WeatherForecastRequest request);
    }
}
