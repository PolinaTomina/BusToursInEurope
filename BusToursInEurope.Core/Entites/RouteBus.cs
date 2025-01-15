namespace BusToursInEurope.Core.Entites
{
    public class RouteBus
    {
        public int Id { get; set; }
        public float Distance { get; set; }
        public string BorderPlace { get; set; }

        public ICollection<WayPoint> WayPoints { get; set; } = new List<WayPoint>();    
    }
}
