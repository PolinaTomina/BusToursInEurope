using BusToursInEurope.Application.Models.CityModel;

namespace BusToursInEurope.Application.Models.HotelModel
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }

        public int CityDtoId { get; set; }
    }
}
