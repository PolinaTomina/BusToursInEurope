
namespace BusToursInEurope.Core.Entites
{
    public class Route
    {
        public int Id { get; set; }

        // List<WayPoint> WayPoints - так у тебя будет
        public string WayPoints { get; set; }
        public float Distance { get; set; }
        public string BorderPlace { get; set; }
    }
}
