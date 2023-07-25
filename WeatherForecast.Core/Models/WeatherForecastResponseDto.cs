using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Contracts.Models
{

    public class WeatherForecastResponseDto
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public double GenerationTime { get; set; }
        public int UtcOffsetSeconds { get; set; }
        public string? Timezone { get; set; }
        public string? TimezoneAbbreviation { get; set; }
        public List<TimelyData> TimelyData { get; set; } = new List<TimelyData>();
    }


    public class TimelyData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public string Value { get; set; }
        public TimeSeriesTypeEnum TimeSeriesType { get; set; }
    }

    public enum TimeSeriesTypeEnum
    {
        Daily,
        Hourly
    }
}

