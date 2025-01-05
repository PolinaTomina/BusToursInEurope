namespace BusToursInEurope.Application.Models.TourModel
{
    public class FullTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Route { get; set; }
        public int NumOfSeats { get; set; }
        public string Description { get; set; }
    }
}
