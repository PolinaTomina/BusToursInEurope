
namespace BusToursInEurope.Core.Entites
{
    public class Review
    {
        // пользователь ссылка

        public string Fio { get; set; }

        public float Rating { get; set; }

        public string Comment { get; set; }

        public string Tour { get; set; } // ссылка?

        public DateTime DateReview { get; set; }
    }
}
