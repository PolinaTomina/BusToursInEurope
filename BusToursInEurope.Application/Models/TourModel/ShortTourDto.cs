namespace BusToursInEurope.Application.Models.TourModel
{
    public class ShortTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
    }
}