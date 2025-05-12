namespace BusToursInEurope.Application.Models.WayPointsModel
{
    public class ShowWayPointsDto
    {
        public int Id { get; set; }

        public string NamePlace { get; set; }

        public int CityId { get; set; }

        public int RouteBusId { get; set; }

        public int HotelId { get; set; }
    }
}
