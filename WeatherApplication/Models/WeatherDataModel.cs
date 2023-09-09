using System.ComponentModel.DataAnnotations;

namespace WeatherApplication.Models
{
    public class WeatherDataModel
    {
        [Required]
        [StringLength(150)]
        public string Location { get; set; }
    }
}
