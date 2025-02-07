namespace BusToursInEurope.Application.Models.ReviewModels
{
    public record CreateReviewDto
    {
        public int TourId { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }
    }
}
