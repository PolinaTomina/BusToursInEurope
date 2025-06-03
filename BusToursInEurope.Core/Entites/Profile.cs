namespace BusToursInEurope.Core.Entites
{
    public class Profile
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? MiddleName { get; set; }

        public string? SurName { get; set; }

        public string? NumPhone { get; set; }

        public string? PassportNumber { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Tour> Tours { get; set; } = new List<Tour>();
    }
}
