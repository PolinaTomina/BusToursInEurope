using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.DbModel
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime PaymentDeadline { get; set; } // срок оплаты
        public int NumOfSeats { get; set; } // количество человек

        public int UserDtoId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
