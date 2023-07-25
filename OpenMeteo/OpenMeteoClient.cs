using Microsoft.Extensions.Options;
using OpenMeteo.Abstractions;
using System.Text;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Contracts.Options;

namespace OpenMeteo
{
    public class OpenMeteoClient : IOpenMeteoClient
    {
        private readonly IWebcaller _webcaller;
        private readonly IOptions<WeatherSourceApiOptions> _weatherSpurceApi;

        public OpenMeteoClient(IWebcaller webcaller, IOptions<WeatherSourceApiOptions> weatherSpurceApi)
        {
            _webcaller = webcaller;
            _weatherSpurceApi = weatherSpurceApi;
        }
        public async Task<WeatherForecastProviderResponse> GetForecastAsync(WeatherForecastRequest weatherForecastRequest)
        {
            var url = GetUrl(weatherForecastRequest);

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };

            return await _webcaller.SendAsync<WeatherForecastProviderResponse>(httpRequestMessage);
        }

        public string GetUrl(WeatherForecastRequest weatherForecastRequest)
        {
            var hourlyVariable = string.Join(',', weatherForecastRequest.HourlyVariables);
            var dailyVariables = string.Join(',', weatherForecastRequest.DailyVariables);
            StringBuilder url = new StringBuilder($"{_weatherSpurceApi.Value.BaseUrl}/v1/forecast?latitude={weatherForecastRequest.Latitude}&longitude={weatherForecastRequest.Longitude}");

            if (weatherForecastRequest.Timezone != null)
            {
                url.Append($"&timezone={weatherForecastRequest.Timezone}");
            }

            if (weatherForecastRequest.TemperatureUnit != null)
            {
                url.Append($"&temperature_unit={weatherForecastRequest.TemperatureUnit}");
            }

            if (weatherForecastRequest.PrecipitationUnit != null)
            {
                url.Append($"&precipitation_unit={weatherForecastRequest.PrecipitationUnit}");
            }

            if (hourlyVariable != string.Empty)
            {
                url.Append($"&hourly={hourlyVariable}");
            }

            if (dailyVariables != string.Empty)
            {
                url.Append($"&daily={dailyVariables}");
            }

            if (weatherForecastRequest.ForecastDays > 0)
            {
                url.Append($"&forecast_days={weatherForecastRequest.ForecastDays}");
            }
            else
            {
                url.Append($"&start_date={weatherForecastRequest.StartDate}");
                url.Append($"&end_date={weatherForecastRequest.EndDate}");
            }

            return url.ToString();
        }
    }
}