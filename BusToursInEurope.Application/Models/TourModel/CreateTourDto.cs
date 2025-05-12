namespace BusToursInEurope.Application.Models.TourModel
{
    public class CreateTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumOfSeats { get; set; }
        public string Description { get; set; }

        public int BusDto { get; set; }

        public int RouteBusDto { get; set; }
    }
}
