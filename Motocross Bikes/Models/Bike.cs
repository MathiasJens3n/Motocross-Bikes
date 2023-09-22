using System.ComponentModel.DataAnnotations;

namespace Motocross_Bikes.Models
{
    public class Bike
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Year is required")]
        [MaxLength(4, ErrorMessage = "Year can't be longer than 4 numbers")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Power is required")]
        public string Power { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Transmission is required")]

        public string Transmission { get; set; }
        [Required(ErrorMessage = "Top speed is required")]

        public double TopSpeed { get; set; }

    }
}
