﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Intefaces;
using WeatherForecast.Contracts.Models;
using WeatherForecast.Services.Validators;

namespace WeatherForecast.Services
{
    public static class ConfigurationExtension
    {

        public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                    .AddSingleton<IValidator<WeatherForecastRequest>, WeatherForecastRequestValidator>();
        }
    }
}
