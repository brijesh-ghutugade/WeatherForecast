using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Core.Implementations;

namespace WeatherForecast.Core
{
    public static class ConfigurationExtension
    {

        public static IServiceCollection AddCore(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                    .AddSingleton<IWebcaller, Webcaller>();
        }
    }
}
