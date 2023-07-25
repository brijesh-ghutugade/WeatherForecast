using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;
using WeatherForecast.DataStore;
using WeatherForecast.DataStore.Repository;
using Xunit;

namespace WeatherForecast.UnitTests
{
    public class WeatherForecastDataStoreFixture
    {
        [Fact]
        public async Task SaveUpdateWeatherForecast_ShouldCallAdd_For_NewForecasts()
        {
            WeatherForecastDataModel existingForecast = null;
            var mockRepo = new Mock<IRepository<WeatherForecastDataModel>>();
            mockRepo.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<WeatherForecastDataModel,bool>>>(), It.IsAny<bool>(), It.IsAny<List<string>>())).ReturnsAsync(existingForecast);
            var weatherForecastDataStore = new WeatheForecastDataStore(mockRepo.Object);

            var dataModel = new WeatherForecastDataModel() { Latitude = 58, Longitude = 13 };
            await weatherForecastDataStore.SaveUpdateWeatherForecastAsync(dataModel);
            mockRepo.Verify(x => x.Add(dataModel), Times.Once());
        }


        [Fact]
        public async Task SaveUpdateWeatherForecast_ShouldCallUpdate_For_ExistingForecasts()
        {
            WeatherForecastDataModel existingForecast = new WeatherForecastDataModel();
            var mockRepo = new Mock<IRepository<WeatherForecastDataModel>>();
            mockRepo.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<WeatherForecastDataModel, bool>>>(), It.IsAny<bool>(), It.IsAny<List<string>>())).ReturnsAsync(existingForecast);
            var weatherForecastDataStore = new WeatheForecastDataStore(mockRepo.Object);

            var dataModel = new WeatherForecastDataModel() { Latitude = 58, Longitude = 13 };
            await weatherForecastDataStore.SaveUpdateWeatherForecastAsync(dataModel);
            mockRepo.Verify(x => x.Update(existingForecast), Times.Once());
        }
    }
}
