using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using WeatherApplication.Models;

namespace WeatherApplication.Repositories
{
    public class GetApiCalls : IWeatherApiGetCalls
    {
        private readonly IConfiguration configuration;

        public GetApiCalls(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<WeatherDataModel> GetWeatherForecastAsync(string location)
        {
           
            using (var client = new HttpClient())
            {
                string authToken = configuration.GetValue<string>("ConnectionStrings:AuthToken");
                string apiKey = configuration.GetValue<string>("ConnectionStrings:ApiKey");

                var request = new HttpRequestMessage(HttpMethod.Get, @"https://api.weatherapi.com/v1/forecast.json?key=" + apiKey + @"=London&days=1&aqi=no&alerts=no");
                request.Headers.Add("Authorization", $"Bearer {authToken}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                
                JObject responseAsJson = JObject.Parse(responseContent);

                var weatherResponseData = GetWeatherData(responseAsJson);

                return weatherResponseData;
            };   
           
        }

        private WeatherDataModel GetWeatherData(JObject dataAsJson)
        {
            var weatherResponseData = new WeatherDataModel();

            weatherResponseData.Location = (string)dataAsJson["location"]["name"];
            weatherResponseData.CurrentTemperature = (string)dataAsJson["current"]["temp_c"];
            weatherResponseData.MaxTemperature = (string)dataAsJson["forecast"]["forecastday"][0]["day"]["maxtemp_c"];
            weatherResponseData.MinTemperature = (string)dataAsJson["forecast"]["forecastday"][0]["day"]["mintemp_c"];
            weatherResponseData.Humidity = (string)dataAsJson["forecast"]["forecastday"][0]["day"]["avghumidity"];
            weatherResponseData.Sunrise = (string)dataAsJson["forecast"]["forecastday"][0]["astro"]["sunrise"];
            weatherResponseData.Sunset = (string)dataAsJson["forecast"]["forecastday"][0]["astro"]["sunset"];

            return weatherResponseData;
        }
    }
}
