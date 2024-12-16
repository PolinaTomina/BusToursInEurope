using System;

namespace BusToursInEurope.Core.Entites
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public DateTime PaymentDate { get; set; } // дата внесения оплаты
        public DateTime PaymentDeadline { get; set; } // срок внесения оплаты
        public int NumOfSeats { get; set; }

        public User User { get; set; }// пользователь ссылка
        public Tour Tour { get; set; }// тур ссылка
    }
}
