using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Contracts.Exceptions
{
    public class BadRequestException: BaseApplicationException
    {
        public BadRequestException(HttpStatusCode httpStatusCode, string message): base(httpStatusCode, message)
        {


        }
    }
}
