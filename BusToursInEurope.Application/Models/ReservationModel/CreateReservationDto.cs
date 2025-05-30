namespace BusToursInEurope.Application.Models.ReservationModel
{
    public class CreateReservationDto
    {
        public DateTime Date { get; set; }
        public int NumOfSeats { get; set; } // количество человек
        public int TourId { get; set; }
    }
}
