using Microsoft.AspNetCore.Http;

namespace BusToursInEurope.Application.Models.TourModel
{
    public class UpdateTourDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Route { get; set; }
        public int? NumOfSeats { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
