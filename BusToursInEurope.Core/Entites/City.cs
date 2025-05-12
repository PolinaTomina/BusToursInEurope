namespace BusToursInEurope.Core.Entites
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Visa { get; set; }

        public ICollection<Hotel> Hotel { get; set; } = new List<Hotel>();
        public ICollection<WayPoint> WayPoints { get; set; } = new List<WayPoint>();
    }
}
