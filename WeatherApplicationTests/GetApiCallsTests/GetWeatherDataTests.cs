using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApplication.Controllers;
using WeatherApplication.Models;
using WeatherApplication.Repositories;

namespace WeatherApplicationTests.GetApiCallsTests
{
    public class GetWeatherDataTests
    {
        private Mock<ILogger<GetApiCalls>> _logger = new Mock<ILogger<GetApiCalls>>();
        private Mock<IWeatherApiGetCalls> _weatherApiGetCalls = new Mock<IWeatherApiGetCalls>();

        // private ILogger<GetApiCalls> _logger;
        private Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        private IWeatherApiGetCalls _weatherApiGet;

        public GetWeatherDataTests()
        {
            _weatherApiGet = new GetApiCalls(_logger.Object, _configuration.Object);
        }

        [Fact]
        public void GetWeatherForecastSuccess()
        {
            //Arrange
            string location = "location";
            var weatherTestModel = new Mock<WeatherDataModel>();

            _weatherApiGetCalls.Setup(x => x.GetWeatherForecastAsync(location)).ReturnsAsync(weatherTestModel.Object);
            //Act

            var result = _weatherApiGet.GetWeatherForecastAsync(location);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void GetWeatherForecastFailLocationInvalid()
        {
            //Assert
            string location = "ddddddddd123";
            var weatherTestModel = new Mock<WeatherDataModel>();
            _weatherApiGetCalls.Setup(x => x.GetWeatherForecastAsync(location));

            //Act
            var result = await _weatherApiGet.GetWeatherForecastAsync(location);

            //Assert
            result.Should().BeNull();

        }


    }
}
