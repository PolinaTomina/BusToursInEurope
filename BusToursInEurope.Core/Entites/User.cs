namespace BusToursInEurope.Core.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string NumPhone {  get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
