using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.DataStore.Context;
using WeatherForecast.DataStore.Repository;

namespace WeatherForecast.DataStore
{
    public static class ConfigurationExtension
    {

        public static IServiceCollection AddWeatherForecastDataStore(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                    .AddDbContext<WeatherForecastDbContext>(options => options.UseInMemoryDatabase("WeatherForecastDatabase"))
                    .AddScoped<DbContext, WeatherForecastDbContext>()
                    .AddScoped<IRepository<WeatherForecastDataModel>, Repository<WeatherForecastDataModel>>()
                    .AddScoped<IWeatherForecastDataStore, WeatheForecastDataStore>();
        }
    }
}
