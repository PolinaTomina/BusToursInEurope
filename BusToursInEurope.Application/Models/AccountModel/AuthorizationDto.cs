using System.ComponentModel.DataAnnotations;

namespace BusToursInEurope.Application.Models.AccountModel
{
    public class AuthorizationDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
