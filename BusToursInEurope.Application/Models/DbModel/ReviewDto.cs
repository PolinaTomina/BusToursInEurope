namespace BusToursInEurope.Application.Models.DbModel
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public int UserDtoId { get; set; }
        public UserDto UserDto { get; set; }
    }
}
