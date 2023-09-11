using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using Moq;
using System.Net.WebSockets;
using WeatherApplication.Controllers;
using WeatherApplication.Models;
using WeatherApplication.Repositories;


namespace WeatherApplicationTests.ControllerTests
{
    public class HomeControllerTests
    {
        
        private Mock<ILogger<HomeController>> _logger = new Mock<ILogger<HomeController>>();
        private Mock<IWeatherApiGetCalls> _weatherApiGetCalls = new Mock<IWeatherApiGetCalls>();

        private HomeController _homeController;
        public HomeControllerTests()
        {
            //SUT
            _homeController = new HomeController(_logger.Object, _weatherApiGetCalls.Object);
        }

        [Fact]
        public void GetIndexViewSuccess()
        {
            //Arrange
         
            var weatherTestModel = new Mock<WeatherDataModel>();
            string errorMessage = string.Empty;
            //Act 
            var result = _homeController.Index(weatherTestModel.Object, errorMessage);
            //Assert 
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public void GetIndexViewWithViewBagSuccess()
        {
            //Arrange
            string errorMessage = "input valid location test";

            //Act
            _homeController.Index(null, errorMessage);

            //Assert
            ((string)_homeController.ViewBag.ErrorMessage).Should().Be(errorMessage);

        }
        
        [Fact]
        public async void HomeControllerGetWeatherDataSuccess()
        {
            //Arrange
            var weatherTestModel = new Mock<WeatherDataModel>();
            string location = "location";

           _weatherApiGetCalls.Setup(x => x.GetWeatherForecastAsync(location)).ReturnsAsync(weatherTestModel.Object);

            //Act
            var result = _homeController.GetWeatherData(weatherTestModel.Object);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();

        }
        

    }
}
