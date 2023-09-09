using WeatherApplication.Models;

namespace WeatherApplication.Repositories
{
    public interface IWeatherApiGetCalls
    {
       public Task<WeatherDataModel> GetWeatherForecastAsync(string location);
    }
}
