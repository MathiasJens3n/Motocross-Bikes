using System.ComponentModel.DataAnnotations;

namespace Motocross_Bikes.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username can't be longer than 20 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
        public string Password { get; set; }
    }
}
