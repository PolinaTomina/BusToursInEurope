namespace BusToursInEurope.Application.Models.HotelModel
{
    public class HotelFilter
    {
        public string? Name { get; set; }
        public double? MinRating { get; set; }
        public double? MaxRating { get; set; }
        public int? CityId { get; set; }
    }
}
