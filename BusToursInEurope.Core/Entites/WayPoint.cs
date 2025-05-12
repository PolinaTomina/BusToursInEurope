namespace BusToursInEurope.Core.Entites
{
    public class WayPoint
    {
        public int Id { get; set; }
        public string NamePlace { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }

        public int RouteBusId { get; set; }
        public RouteBus? RouteBus { get; set; }

        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
    }
}
