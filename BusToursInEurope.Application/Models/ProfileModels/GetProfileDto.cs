using BusToursInEurope.Application.Models.UserModel;

namespace BusToursInEurope.Application.Models.ProfileModels
{
    public class GetProfileDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? MiddleName { get; set; }

        public string? SurName { get; set; }

        public string? NumPhone { get; set; }

        public string? PassportNumber { get; set; }

        public UserDto User { get; set; }
    }
}
