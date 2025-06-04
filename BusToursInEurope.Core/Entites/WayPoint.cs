namespace BusToursInEurope.Core.Entites
{
    public class WayPoint
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int RouteBusId { get; set; }
        public RouteBus? RouteBus { get; set; }
    }
}
