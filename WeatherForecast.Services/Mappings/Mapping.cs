using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;

namespace WeatherForecast.Services.Mappings
{
    public class Mapping : Profile

    {
        public Mapping()
        {
            CreateMap<WeatherForecastResponseDto, WeatherForecastDataModel>().ReverseMap();
            CreateMap<TimelyData, TimelyDataModel>().ReverseMap(); 
        }
    }
}
