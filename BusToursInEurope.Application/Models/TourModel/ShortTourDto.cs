using Microsoft.AspNetCore.Http;

namespace BusToursInEurope.Application.Models.TourModel
{
    public class ShortTourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FirstImageLink { get; set; }
        public int ReservationCount { get; set; }
        public double Rating { get; set; }
        public bool IsLiked { get; set; }
    }
}