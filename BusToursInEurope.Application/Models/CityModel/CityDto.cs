namespace BusToursInEurope.Application.Models.CityModel
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Visa { get; set; }

        public ICollection<int> HotelIds { get; set; } = new List<int>();
        public ICollection<int> WayPointIds { get; set; } = new List<int>();
    }
}
