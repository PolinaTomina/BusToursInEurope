namespace BusToursInEurope.Application.Models.ReservationModel
{
    public class CreateReservationDto
    {
        public DateTime Date { get; set; }
        public DateTime PaymentDate { get; set; } // дата оплаты
        public DateTime PaymentDeadline { get; set; } // срок оплаты
        public int NumOfSeats { get; set; } // количество человек

        public int UserId { get; set; }
        public int TourId { get; set; }
    }
}
