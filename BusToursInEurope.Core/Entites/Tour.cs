
namespace BusToursInEurope.Core.Entites
{
    public class Tour
    {
        public string Name { get; set; }

        public float Price { get; set; }
        
        //транспорт ссылка

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //клиенты (пользователи)

        public string Route { get; set; }

        //описание
        //ссылки на изображения

        public string NumOfSeats { get; set; }
    }
}
