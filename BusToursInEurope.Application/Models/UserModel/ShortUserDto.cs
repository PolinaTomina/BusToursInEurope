namespace BusToursInEurope.Application.Models.UserModel
{
    public class ShortUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login {  get; set; }
        public bool IsLocked { get; set; }
    }
}
