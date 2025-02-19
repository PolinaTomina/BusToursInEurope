namespace BusToursInEurope.Application.Models.CityModel
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Visa { get; set; }

        public int HotelId { get; set; }
        public int WayPointDtoId { get; set; }
    }
}
