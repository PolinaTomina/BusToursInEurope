using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Models.DbModel
{
    public class RouteBusDto
    {
        public int Id { get; set; }
        public float Distance { get; set; }

        public ICollection<WayPointDto> WayPointsDto { get; set; } = new List<WayPointDto>();
    }
}
