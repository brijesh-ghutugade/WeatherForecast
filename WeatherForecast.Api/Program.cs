using OpenMeteo;
using WeatherForecast.DataStore;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Services;
using WeatherForecast.Services.Abstractions;
using WeatherForecast.Contracts.Options;
using AutoMapper;
using WeatherForecast.Services.Mappings;
using WeatherForecast.Api.Middelewares;
using WeatherForecast.Core;

namespace WeatherForecast.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddWeatherForecastDataStore();
            builder.Services.AddWeatherServiceProvider();
            builder.Services.AddServices();
            builder.Services.AddCore();

            builder.Services.Configure<WeatherSourceApiOptions>(builder.Configuration.GetSection(WeatherSourceApiOptions.WeatherSourceApi));

            //Automapper
            builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mapping());

            }).CreateMapper());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}