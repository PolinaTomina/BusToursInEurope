
namespace BusToursInEurope.Core.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fio { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NumPhone {  get; set; }

        //роли (ссылки на роли)
    }
}
