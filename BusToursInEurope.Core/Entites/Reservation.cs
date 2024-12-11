using System;

namespace BusToursInEurope.Core.Entites
{
    public class Reservation
    {
        public DateTime Data { get; set; }

        public DateTime PaymentDate { get; set; } // дата внесения оплаты

        public DateTime PaymentDeadline { get; set; } // срок внесения оплаты

        // пользователь ссылка
        // тур ссылка

        public int NumOfSeats { get; set; }
    }
}
