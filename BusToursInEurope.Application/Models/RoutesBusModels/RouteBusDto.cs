using BusToursInEurope.Application.Models.DbModel;

namespace BusToursInEurope.Application.Models.RoutesBusModels
{
    public class RouteBusDto
    {
        public int Id { get; set; }
        public float Distance { get; set; }

        public ICollection<WayPointDto> WayPointsDto { get; set; } = new List<WayPointDto>();
    }
}