using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Intefaces;

namespace WeatherForecast.Core.Implementations
{
    public class Webcaller : IWebcaller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<Webcaller> _logger;
        public Webcaller(IHttpClientFactory httpClientFactory, ILogger<Webcaller> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }


        private async Task<T> ParseResponse<T>(HttpResponseMessage response)
        {
            var resultContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(resultContent); ;
        }

        public async Task<T> SendAsync<T>(HttpRequestMessage requestMessage)
        {
            try
            {
                using var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                return await ParseResponse<T>(response);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                throw;
            }           
        }

        public async Task<T> GetAsync<T>(string requestUri)
        {
            try
            {
                using var response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                return await ParseResponse<T>(response);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
                throw;
            }           
        }
    }
}
