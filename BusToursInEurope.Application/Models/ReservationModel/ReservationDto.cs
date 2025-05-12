using BusToursInEurope.Application.Models.DbModel;

namespace BusToursInEurope.Application.Models.ReservationModel
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; } // дата оплаты
        public DateTime PaymentDeadline { get; set; } // срок оплаты
        public int NumOfSeats { get; set; } // количество человек

        public int UserDtoId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
