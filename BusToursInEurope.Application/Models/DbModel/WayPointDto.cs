using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Application.Models.RoutesBusModels;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.DbModel
{
    public class WayPointDto
    {
        public int Id { get; set; }
        public string NamePlace { get; set; }

        public int CityDtoId { get; set; }
        public CityDto CityDto { get; set; }

        public int RouteBusDtoId { get; set; }
        public RouteBusDto RouteBusDto { get; set; }

        public int HotelDtoId { get; set; }
        public HotelDto HotelDto { get; set; }
    }
}
