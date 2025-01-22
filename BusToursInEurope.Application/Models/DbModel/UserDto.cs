using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.DbModel
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string NumPhone { get; set; }

        public bool IsAdmin { get; set; } = false; // Администратор
        public bool IsUser { get; set; } = true;  // Обычный пользователь (по умолчанию)

        public ICollection<ReservationDto> ReservationsDto { get; set; } = new List<ReservationDto>();
        public ICollection<ReviewDto> ReviewsDto { get; set; } = new List<ReviewDto>();
    }
}
