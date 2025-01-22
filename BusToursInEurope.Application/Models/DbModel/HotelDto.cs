using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.DbModel
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }

        public int CityDtoId { get; set; }
        public CityDto CityDto { get; set; }
    }
}
