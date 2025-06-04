namespace BusToursInEurope.Application.Models.RoutesBusModels
{
    public class CreateRouteBusDto
    {
        public string Name { get; set; }
        public float Distance { get; set; }
        public List<CreateWayPointDto> WayPoints { get; set; }
    }
}
