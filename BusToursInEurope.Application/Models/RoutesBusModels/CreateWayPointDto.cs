namespace BusToursInEurope.Application.Models.RoutesBusModels
{
    public record CreateWayPointDto
    {
        public string NamePlace { get; set; }
        public int CityId { get; set; }
        public int? HotelId { get; set; }
    }
}
