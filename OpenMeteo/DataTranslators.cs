using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;

namespace OpenMeteo
{
    public static class DataTranslators
    {
        public static WeatherForecastResponseDto ToWeatherForecastResponse(this WeatherForecastProviderResponse providerResponse)
        {
            var weatherForecastResponse = new WeatherForecastResponseDto()
            {
                Latitude = providerResponse.latitude,
                Longitude = providerResponse.longitude,
                Elevation = providerResponse.elevation,
                GenerationTime = providerResponse.generationtime_ms,
                Timezone = providerResponse.timezone,
                TimezoneAbbreviation = providerResponse.timezone_abbreviation,
                UtcOffsetSeconds = providerResponse.utc_offset_seconds
            };

            TranslateData(providerResponse, weatherForecastResponse);

            return weatherForecastResponse;
        }

        private static void TranslateData(WeatherForecastProviderResponse providerResponse, WeatherForecastResponseDto weatherForecastResponse)
        {
            var dailyTimeSeries = providerResponse.daily.time;

            if (dailyTimeSeries != null)
            {

                foreach (var propInfo in providerResponse.daily_units.GetType().GetProperties())
                {
                    if (!propInfo.Name.Equals("time"))
                    {

                        IEnumerable? values = (providerResponse.daily.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals(propInfo.Name))?.GetValue(providerResponse.daily)) as IEnumerable;

                        if (values != null)
                        {
                            int index = 0;
                            foreach (var value in values)
                            {
                                weatherForecastResponse.TimelyData.Add(new TimelyData()
                                {
                                    TimeSeriesType = TimeSeriesTypeEnum.Daily,
                                    Name = propInfo.Name,
                                    Value = value?.ToString(),
                                    Time = dailyTimeSeries[index]
                                });
                                index++;
                            }
                        }
                    }
                }
            }


            var horlyTimeSeries = providerResponse.hourly.time;

            if (horlyTimeSeries != null)
            {

                foreach (var propInfo in providerResponse.hourly_units.GetType().GetProperties())
                {
                    if (!propInfo.Name.Equals("time"))
                    {
                        IEnumerable? values = (providerResponse.hourly.GetType().GetProperties().FirstOrDefault(p => p.Name.Equals(propInfo.Name))?.GetValue(providerResponse.hourly)) as IEnumerable;

                        if (values != null)
                        {
                            int index = 0;
                            foreach (var value in values)
                            {
                                weatherForecastResponse.TimelyData.Add(new TimelyData()
                                {
                                    TimeSeriesType = TimeSeriesTypeEnum.Hourly,
                                    Name = propInfo.Name,
                                    Value = value?.ToString(),
                                    Time = horlyTimeSeries[index]
                                });
                                index++;
                            }
                        }
                    }
                }
            }
        }
    }
}
