
namespace BusToursInEurope.Core.Entites
{
    public class Review
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DateReview { get; set; }

        public User User { get; set; }// пользователь ссылка
        public Tour Tour { get; set; } // ссылка?
    }
}
