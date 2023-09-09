using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public class WeatherDataModel
    {
        [Required]
        [StringLength(150)]
        public string Location { get; set; }

        public string CurrentTemperature { get; set; }

        public string MinTemperature { get; set; }

        public string MaxTemperature { get; set; }

        public string Humidity { get; set; }

        public string Sunrise { get; set; }

        public string Sunset { get; set;}
    }
}
