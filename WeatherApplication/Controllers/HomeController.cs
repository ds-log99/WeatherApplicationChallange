using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(WeatherDataModel? weatherData)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetWeatherData(WeatherDataModel weatherData) 
        {
       
            return RedirectToAction("Index", weatherData);
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