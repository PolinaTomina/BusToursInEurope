namespace BusToursInEurope.Application.Models.HotelModel
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }

        public int CityId { get; set; }
    }
}
