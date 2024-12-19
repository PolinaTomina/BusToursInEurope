
namespace BusToursInEurope.Core.Entites
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Route { get; set; }
        public int NumOfSeats { get; set; }
        public string Description { get; set; }

        //public Transport Transport { get; set; } // ссылка?

        public List<User> users;//клиенты (пользователи)
        //ссылка на изображения
    }
}
