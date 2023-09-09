using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApplication.Models;
using WeatherApplication.Repositories;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherApiGetCalls weatherApiGet;

        public HomeController(ILogger<HomeController> logger, IWeatherApiGetCalls weatherApiGet)
        {
            _logger = logger;
            this.weatherApiGet = weatherApiGet;
        }

        [HttpGet]
        public IActionResult Index(WeatherDataModel? weatherData)
        {
            return View(weatherData);
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherData(WeatherDataModel weatherData) 
        {
            try 
            {
                WeatherDataModel requestWeatherData = await weatherApiGet.GetWeatherForecastAsync(weatherData.Location);
                return RedirectToAction("Index", requestWeatherData);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error occured when processing the request it has not been successful");
                return BadRequest(ex.Message);
            }
           
         
        }

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        */
    }
}