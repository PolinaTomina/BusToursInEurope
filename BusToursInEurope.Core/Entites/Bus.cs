
namespace BusToursInEurope.Core.Entites
{
    public class Bus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfSeats { get; set; }

        public ICollection<Tour> Tours { get; set; } = new List<Tour>();
    }
}
