using AutoMapper;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Services;
using Xunit;

namespace WeatherForecast.UnitTests
{
    public class WeatherServiceFixture
    {
        [Fact]
        public async Task GetWeatherForecast_ShouldCall_Provider_And_DataStore()
        {
            var request = new WeatherForecastRequest()
            {
                Latitude = 58,
                Longitude = 13,
                ForecastDays = 3,
                Timezone = "GMT",
                HourlyVariables = new List<string>() { "temperature_2m" },
                DailyVariables = new List<string>() { "weathercode" },
                TemperatureUnit = "fahrenheit"
            };
            var weatherForecastResponseDto = new WeatherForecastResponseDto();
            var weatherForecastDataModel = new WeatherForecastDataModel();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x=>x.Map<WeatherForecastDataModel>(weatherForecastResponseDto)).Returns(weatherForecastDataModel);

            var mockProvider = new Mock<IWeatherForecastProvider>();
            mockProvider.Setup(x => x.GetWeatherForecastAsync(It.Is<WeatherForecastRequest>(r=>r.Latitude==58 && r.Longitude==13))).ReturnsAsync(weatherForecastResponseDto); 

            var mockDataStore = new Mock<IWeatherForecastDataStore>();
            mockDataStore.Setup(x=>x.SaveUpdateWeatherForecastAsync(It.Is<WeatherForecastDataModel>(w=>w.Latitude==58 && w.Longitude == 13))).ReturnsAsync(weatherForecastDataModel);


            var mockValidator = new Mock<IValidator<WeatherForecastRequest>>();
            mockValidator.Setup(x => x.ValidateAsync(request, CancellationToken.None)).ReturnsAsync(new FluentValidation.Results.ValidationResult() { });

            var weatherService = new WeatherService(mockProvider.Object, mockValidator.Object, mockDataStore.Object, mockMapper.Object);

            await weatherService.GetWeatherForecastAsync(request);

            mockProvider.Verify(x => x.GetWeatherForecastAsync(request), Times.Once);
            mockDataStore.Verify(x => x.SaveUpdateWeatherForecastAsync(weatherForecastDataModel), Times.Once);
        }
    }
}
