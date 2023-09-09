using Newtonsoft.Json;

namespace WeatherApplication.Repositories
{
    public class GetApiCalls : IWeatherApiGetCalls
    {
        private readonly IConfiguration configuration;

        public GetApiCalls(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task GetWeatherForecastAsync(string location)
        {
            string authToken = configuration.GetValue<string>("ConnectionStrings:AuthToken");
            string apiKey = configuration.GetValue<string>("ConnectionStrings:ApiKey");
           
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, @"https://api.weatherapi.com/v1/forecast.json?key=" + apiKey + @"=London&days=1&aqi=no&alerts=no");
                request.Headers.Add("Authorization", $"Bearer {authToken}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                
                var responseAsJson = JsonConvert.SerializeObject(responseContent);               
            };
               
           

        }
    }
}
