namespace BusToursInEurope.Core.Entites
{
    public class Review
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TourId { get; set; }
        public Tour Tour { get; set; } 
    }
}
