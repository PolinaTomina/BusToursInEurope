namespace BusToursInEurope.Application.Models.ReservationModel
{
    public class CreateReservationDto
    {
        public int NumOfSeats { get; set; } // количество человек
        public int TourId { get; set; }
    }
}
