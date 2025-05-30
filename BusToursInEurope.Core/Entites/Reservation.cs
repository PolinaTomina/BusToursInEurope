namespace BusToursInEurope.Core.Entites
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? PaymentDate { get; set; } // дата оплаты
        public DateTime PaymentDeadline { get; set; } // срок оплаты
        public int NumOfSeats { get; set; } // количество человек

        public int UserId { get; set; }
        public User User { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
