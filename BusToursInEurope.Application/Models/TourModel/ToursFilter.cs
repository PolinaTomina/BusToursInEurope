namespace BusToursInEurope.Application.Models.TourModel
{
    public class ToursFilter
    {
        public string? Country { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
