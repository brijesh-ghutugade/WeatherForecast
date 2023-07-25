using FluentValidation;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Services.Abstractions;
using WeatherForecast.Contracts.Exceptions;
using System.Net;
using AutoMapper;

namespace WeatherForecast.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherForecastProvider _weatherForecastProvider;
        private readonly IValidator<WeatherForecastRequest> _requestValidator;
        private readonly IWeatherForecastDataStore _weatherForecastDataStore;
        private readonly IMapper _mapper;

        public WeatherService(IWeatherForecastProvider weatherForecastProvider, IValidator<WeatherForecastRequest> requestValidator, IWeatherForecastDataStore weatherForecastDataStore, IMapper mapper)
        {
            _weatherForecastProvider = weatherForecastProvider;
            _requestValidator = requestValidator;
            _weatherForecastDataStore = weatherForecastDataStore;
            _mapper = mapper;
        }

        public async Task<bool> DeleteWeatherForecastAsync(int id)
        {
            await _weatherForecastDataStore.DeleteWeatherForecastAsync(id);
            return true;
        }

        public async Task<WeatherForecastResponseDto> GetWeatherForecastAsync(WeatherForecastRequest weatherForecastRequest)
        {
            var validationResult = await _requestValidator.ValidateAsync(weatherForecastRequest);
            if (validationResult.IsValid == false)
            {
                var errorMessages = string.Join('&', validationResult.Errors.Select(e => e.ErrorMessage));
                throw new BaseApplicationException(HttpStatusCode.BadRequest, errorMessages);
            }
            var weatherForecastResponse = await _weatherForecastProvider.GetWeatherForecastAsync(weatherForecastRequest);
            var weatherForecastDataModel = _mapper.Map<WeatherForecastDataModel>(weatherForecastResponse);

            await _weatherForecastDataStore.SaveUpdateWeatherForecastAsync(weatherForecastDataModel);
            return weatherForecastResponse;
        }
       
        public async Task<List<WeatherForecastResponseDto>> GetForecastHistoryAsync()
        {
            var forecastHistoryData = await _weatherForecastDataStore.GetForecastHistoryAsync();
            List<WeatherForecastResponseDto> weatherForecastHistory = _mapper.Map<List<WeatherForecastResponseDto>>(forecastHistoryData);

            return weatherForecastHistory;
        }
    }
}
