namespace BusToursInEurope.Core.Entites
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfSeats { get; set; }
        public string Description { get; set; }
        public List<string> ImageLinks {  get; set; } = new List<string>();

        public int BusId { get; set; }
        public Bus Bus { get; set; }

        public int RouteBusId { get; set; }
        public RouteBus RouteBus { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
