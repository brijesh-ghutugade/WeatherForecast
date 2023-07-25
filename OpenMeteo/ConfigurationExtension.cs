using Microsoft.Extensions.DependencyInjection;
using OpenMeteo.Abstractions;
using WeatherForecast.Contracts.Intefaces;

namespace OpenMeteo
{
    public static class ConfigurationExtension
    {

        public static IServiceCollection AddWeatherServiceProvider(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                    .AddSingleton<IWeatherForecastProvider, WeatherForecastProvider>()
                    .AddSingleton<IOpenMeteoClient, OpenMeteoClient>();
        }
    }
}
