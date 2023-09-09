namespace WeatherApplication.Repositories
{
    public interface IWeatherApiGetCalls
    {
       public Task GetWeatherForecastAsync(string location);
    }
}
