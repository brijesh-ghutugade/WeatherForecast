using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;

namespace WeatherForecast.DataStore.Context
{
    public class WeatherForecastDbContext : DbContext
    {
        public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options) { }
        public WeatherForecastDbContext() { }
        public DbSet<WeatherForecastDataModel> WeatherForecastData { get; set; }
        public DbSet<TimelyDataModel> TimelyData { get; set; }        
    }
}
