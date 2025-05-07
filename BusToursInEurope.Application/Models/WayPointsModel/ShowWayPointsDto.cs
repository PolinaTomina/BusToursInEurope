namespace BusToursInEurope.Application.Models.WayPointsModel
{
    public class ShowWayPointsDto
    {
        public int Id { get; set; }

        public string NamePlace { get; set; }

        public int CityDtoId { get; set; }

        public int RouteBusDtoId { get; set; }

        public int HotelDtoId { get; set; }
    }
}
