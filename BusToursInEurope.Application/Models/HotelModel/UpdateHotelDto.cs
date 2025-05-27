namespace BusToursInEurope.Application.Models.HotelModel
{
    public class UpdateHotelDto
    {
        public string? Name { get; set; }
        public double? Rating { get; set; }

        public int? CityId { get; set; }
    }
}
