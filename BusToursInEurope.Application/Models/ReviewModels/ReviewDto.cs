namespace BusToursInEurope.Application.Models.ReviewModels
{
    public record ReviewDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
    }
}