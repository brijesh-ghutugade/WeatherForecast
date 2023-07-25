using System.Reflection.Metadata;

namespace WeatherForecast.Contracts.Models
{
    public class WeatherForecastDataModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? GenerationTime { get; set; }
        public int? UtcOffsetSeconds { get; set; }
        public string? Timezone { get; set; }
        public string? TimezoneAbbreviation { get; set; }
        public double? Elevation { get; set; }
        public virtual ICollection<TimelyDataModel> TimelyData { get; } = new List<TimelyDataModel>();

    }
    public class TimelyDataModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Time { get; set; }
        public string? Value { get; set; }
        public TimeSeriesTypeEnum TimeSeriesType { get; set; }
        public int WeatherForecastDataModelId { get; set; }
        public WeatherForecastDataModel WeatherForecast { get; set; } = null!;
    }
}