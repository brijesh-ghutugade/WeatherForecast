using System;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Services.Validators;
using Xunit;

namespace WeatherForecast.UnitTests
{
    public class WeatherForecastValidatorFixture
    {
        [Theory]       
        [InlineData(-345, 345,new string[]{ "temp"}, new string[]{ "we" }, "c", "p", "g", null, null, 0)]
        [InlineData(-345, 345, new string[] { "temp" }, new string[] { "we" }, "c", "p", "g", "23-07-2023", "25-07-2023" , 8)]
        public async void ValidateAsync_ShouldReturnFalse_WithInvalidRequest(double latitude, double longitude, string[] hourlyVariables, string[] dailyVariables, string temperatureUnit, string precipitationUnit, string timezone, string startDate, string enddate, int forecastDays)
        {
            DateTime? start = startDate != null ? DateTime.Parse(startDate) : null;
            DateTime? end = enddate != null ? DateTime.Parse(enddate) : null;

            var request = GetRequest(latitude, longitude, hourlyVariables.ToList(), dailyVariables.ToList(), temperatureUnit, precipitationUnit, timezone, start, end, forecastDays);

            var result = await new WeatherForecastRequestValidator().ValidateAsync(request);

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }


        [Theory]
        [InlineData(58, 15, new string[] { "temperature_2m" }, new string[] { "weathercode" }, "fahrenheit", "inch", "GMT", "23-07-2023", "25-07-2023", 0)]

        public async void ValidateAsync_ShouldReturnTrue_WithValidRequest(double latitude, double longitude, string[] hourlyVariables, string[] dailyVariables, string temperatureUnit, string precipitationUnit, string timezone, string startDate, string endDate, int forecastDays)
        {
            DateTime? start = startDate != null ? DateTime.Parse(startDate) : null;
            DateTime? end = endDate != null ? DateTime.Parse(endDate) : null;
            var request = GetRequest(latitude, longitude, hourlyVariables.ToList(), dailyVariables.ToList(), temperatureUnit, precipitationUnit, timezone, start, end, forecastDays);

            var result = await new WeatherForecastRequestValidator().ValidateAsync(request);

            Assert.True(result.IsValid);
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<string> HourlyVariables { get; set; } = new List<string>();
        public List<string> DailyVariables { get; set; } = new List<string>();
        public string? TemperatureUnit { get; set; }
        public string? PrecipitationUnit { get; set; }
        public string? Timezone { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ForecastDays { get; set; }

        private WeatherForecastRequest GetRequest(double latitude, double longitude, List<string> hourlyVariables, List<string> dailyVariables, string temperatureUnit, string precipitationUnit, string timezone, DateTime? startDate, DateTime? endDate, int forecastDays)
        {
            return new WeatherForecastRequest()
            {
                Latitude = latitude,
                Longitude = longitude,
                HourlyVariables = hourlyVariables,
                DailyVariables = dailyVariables,
                PrecipitationUnit = precipitationUnit,
                Timezone = timezone,
                StartDate = startDate,
                EndDate = endDate,
                ForecastDays = forecastDays,
                TemperatureUnit = temperatureUnit,
            };
        }
    }
}