
namespace BusToursInEurope.Core.Entites
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Visa { get; set; }

        public WayPoint WayPoint { get; set; }// WayPoints ссылка
    }
}
