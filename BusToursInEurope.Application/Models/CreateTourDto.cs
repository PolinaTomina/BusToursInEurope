namespace BusToursInEurope.Application.Models
{
    public class CreateTourDto
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Route { get; set; }
        public int NumOfSeats { get; set; }
        public string Description { get; set; }
    }
}
