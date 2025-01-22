namespace BusToursInEurope.Application.Models.DbModel
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DateReview { get; set; }

        public int UserDtoId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
