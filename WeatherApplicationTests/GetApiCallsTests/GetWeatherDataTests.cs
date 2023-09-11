using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private ILogger<GetApiCalls> _logger;
        private IConfiguration _configuration;
        private IWeatherApiGetCalls _weatherApiGet;

        public GetWeatherDataTests()
        {
            //Dependencies 
            _logger = A.Fake<ILogger<GetApiCalls>>();
            _configuration = A.Fake<IConfiguration>();
            
            _weatherApiGet = new GetApiCalls(_logger, _configuration);
        }

        [Fact]
        public void GetWeatherForecastSuccess()
        {
            //Arrange
            string location = "location";
            //Act
            var result = _weatherApiGet.GetWeatherForecastAsync(location);
                
            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void GetWeatherForecastFail()
        {
            //Assert
            string location = "ASDADSADASSAD";
            var weatherTestModel = A.Fake<WeatherDataModel>();

            //Act

            var result = _weatherApiGet.GetWeatherForecastAsync(location);

            //Assert
            result.Equals(weatherTestModel);

        }


    }
}
