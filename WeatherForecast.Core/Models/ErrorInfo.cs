using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Contracts.Models
{
    public class ErrorInfo
    {
        public ErrorInfo(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
