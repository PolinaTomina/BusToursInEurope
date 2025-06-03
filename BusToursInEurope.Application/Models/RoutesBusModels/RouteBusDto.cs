using BusToursInEurope.Application.Models.WayPointsModel;

namespace BusToursInEurope.Application.Models.RoutesBusModels
{
    public class RouteBusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Distance { get; set; }

        public ICollection<WayPointDto> WayPointsDto { get; set; } = new List<WayPointDto>();
    }
}