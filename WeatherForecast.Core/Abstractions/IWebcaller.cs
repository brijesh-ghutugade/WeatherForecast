using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Contracts.Intefaces
{
    public interface IWebcaller
    {
        Task<T> SendAsync<T>(HttpRequestMessage requestMessage);
        Task<T> GetAsync<T>(string requestUri);
    }
}
