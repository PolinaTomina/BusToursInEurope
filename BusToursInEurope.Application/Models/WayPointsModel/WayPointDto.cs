using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Models.RoutesBusModels;

namespace BusToursInEurope.Application.Models.WayPointsModel
{
    public class WayPointDto
    {
        public int Id { get; set; }
        public string NamePlace { get; set; }

        public int CityId { get; set; }
        public ShowCityDto CityDto { get; set; }

        public int RouteBusId { get; set; }
        public RouteBusDto RouteBusDto { get; set; }

        public int HotelId { get; set; }
        public HotelDto HotelDto { get; set; }
    }
}
