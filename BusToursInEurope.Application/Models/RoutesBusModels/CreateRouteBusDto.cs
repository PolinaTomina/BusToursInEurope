namespace BusToursInEurope.Application.Models.RoutesBusModels
{
    public class CreateRouteBusDto
    {
        public float Distance { get; set; }
        public List<int> WaypointsId { get; set; }
    }
}
