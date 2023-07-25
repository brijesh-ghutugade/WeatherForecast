using OpenMeteo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WeatherForecast.UnitTests
{
    public class DataTranslatorsFixture
    {
        [Fact]
        public void DataTranslators_ToWeatherForecastResponse_ShouldConvertTo_CorrectResponse()
        {
            var weatherForecastProviderResponse = new WeatherForecastProviderResponse();
            weatherForecastProviderResponse.daily = new Daily();
            weatherForecastProviderResponse.daily_units = new DailyUnits();
            weatherForecastProviderResponse.daily.time = new List<string>();
            weatherForecastProviderResponse.daily.time.Add("24-07-2023");
            weatherForecastProviderResponse.daily.time.Add("24-07-2023");
            weatherForecastProviderResponse.daily.weathercode = new List<int>();
            weatherForecastProviderResponse.daily.weathercode.Add(95);
            weatherForecastProviderResponse.daily.weathercode.Add(61);

            weatherForecastProviderResponse.hourly = new Hourly();
            weatherForecastProviderResponse.hourly_units = new HourlyUnits();
            weatherForecastProviderResponse.hourly.time = new List<string>();
            weatherForecastProviderResponse.hourly.time.Add("24-07-2023T01:00");
            weatherForecastProviderResponse.hourly.time.Add("24-07-2023T02:00");
            weatherForecastProviderResponse.hourly.weathercode = new List<int>();
            weatherForecastProviderResponse.hourly.weathercode.Add(95);
            weatherForecastProviderResponse.hourly.weathercode.Add(61);

            var translatedResponse = DataTranslators.ToWeatherForecastResponse(weatherForecastProviderResponse);

            Assert.Equal(4, translatedResponse.TimelyData.Count);
        }
    }
}
