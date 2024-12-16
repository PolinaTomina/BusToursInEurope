
namespace BusToursInEurope.Core.Entites
{
    public class Transport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfSeats { get; set; }

        public Tour Tour { get; set; }// тур ссылка
    }
}
