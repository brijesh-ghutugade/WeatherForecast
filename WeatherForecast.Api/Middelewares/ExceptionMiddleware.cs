using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using WeatherForecast.Contracts.Exceptions;
using WeatherForecast.Contracts.Models;

namespace WeatherForecast.Api.Middelewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseApplicationException e)
            {
                _logger.LogError($"Application Exception: {JsonConvert.SerializeObject(e)}");
                await WriteErrorResponse(context, new ErrorInfo(e.StatusCode, e.ErrorMessage));
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Http Request Exception : {JsonConvert.SerializeObject(e)}");
                await WriteErrorResponse(context, new ErrorInfo(e.StatusCode ?? HttpStatusCode.InternalServerError, e.Message));
            }
            catch (Exception e)
            {
                _logger.LogError($"Unhandled Excep: {JsonConvert.SerializeObject(e)}");
                await WriteErrorResponse(context, new ErrorInfo(HttpStatusCode.InternalServerError, "Internal Server Error Occured!"));
            }
        }
        private async Task WriteErrorResponse(HttpContext context, ErrorInfo errorInfo)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorInfo.StatusCode;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorInfo));
        }
    }
}
