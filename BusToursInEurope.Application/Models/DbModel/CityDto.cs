using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.DbModel
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public bool Visa { get; set; }

        public ICollection<HotelDto> HotelDto { get; set; } = new List<HotelDto>();
        public ICollection<WayPointDto> WayPointsDto { get; set; } = new List<WayPointDto>();
    }
}
