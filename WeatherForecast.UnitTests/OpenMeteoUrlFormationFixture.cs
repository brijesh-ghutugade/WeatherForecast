using Microsoft.Extensions.Options;
using Moq;
using OpenMeteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Contracts.Options;
using Xunit;

namespace WeatherForecast.UnitTests
{
    public class OpenMeteoUrlFormationFixture
    {
        [Fact]
        public void GetUrl_ShouldRetun_CorrectUrl()
        {
            var mockOptions = new Mock<IOptions<WeatherSourceApiOptions>>();
            mockOptions.Setup(x => x.Value).Returns(new WeatherSourceApiOptions() { BaseUrl = "https://api.open-meteo.com" });
            var mockWebcaller = new Mock<IWebcaller>();
            var client = new OpenMeteoClient(mockWebcaller.Object, mockOptions.Object);
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
            var url = client.GetUrl(request);

            Assert.Equal("https://api.open-meteo.com/v1/forecast?latitude=58&longitude=13&timezone=GMT&temperature_unit=fahrenheit&hourly=temperature_2m&daily=weathercode&forecast_days=3", url);
        }
    }
}
