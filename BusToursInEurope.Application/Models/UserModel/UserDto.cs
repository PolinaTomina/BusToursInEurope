using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.ReviewModels;

namespace BusToursInEurope.Application.Models.UserModel
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public ICollection<ReservationDto> ReservationsDto { get; set; } = new List<ReservationDto>();
        public ICollection<ReviewDto> ReviewsDto { get; set; } = new List<ReviewDto>();
    }
}
