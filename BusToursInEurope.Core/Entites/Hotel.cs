
namespace BusToursInEurope.Core.Entites
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
