using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Contracts.Options
{
    public class WeatherSourceApiOptions
    {
        public const string WeatherSourceApi = "WeatherSourceApi";
        public string BaseUrl { get; set; }
    }
}
