using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using WeatherApplication.Controllers;
using WeatherApplication.Models;
using WeatherApplication.Repositories;

namespace WeatherApplicationTests.ControllerTests
{
    public class HomeControllerTests
    {
        
        private ILogger<HomeController> _logger;
        private IWeatherApiGetCalls _weatherApiGetCalls;
        private HomeController _homeController;
        public HomeControllerTests()
        {
            //Dependencies 
            _logger = A.Fake<ILogger<HomeController>>();
            _weatherApiGetCalls = A.Fake<IWeatherApiGetCalls>();

            //SUT
            _homeController = new HomeController(_logger, _weatherApiGetCalls);
        }

        [Fact]
        public void GetIndexViewSuccess()
        {
            //Arrange
            var weatherTestModel = A.Fake<WeatherDataModel>();
            string errorMessage = string.Empty;
            //Act 
            var result = _homeController.Index(weatherTestModel, errorMessage);
            //Assert 
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public void GetIndexViewWithViewBagSuccess()
        {
            string errorMessage = "input valid location test";

            ((string)_homeController.ViewBag.ErrorMessage).Should().Be(errorMessage);

        }

        [Fact]
        public void HomeControllerGetWeatherDataSuccess()
        {
            //Arrange
            var weatherTestModel = A.Fake<WeatherDataModel>();
            string location = "location";
            var weatherFakeDateResult =  A.CallTo(() => _weatherApiGetCalls.GetWeatherForecastAsync(location)).Returns(weatherTestModel);

            //Act
            var result = _homeController.GetWeatherData(weatherTestModel);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }


    }
}
