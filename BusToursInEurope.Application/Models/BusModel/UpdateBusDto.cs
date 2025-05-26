namespace BusToursInEurope.Application.Models.BusModel
{
    public class UpdateBusDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? NumOfSeats { get; set; }
    }
}
