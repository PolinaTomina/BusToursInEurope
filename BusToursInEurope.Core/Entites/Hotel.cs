
namespace BusToursInEurope.Core.Entites
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }

        public City City { get; set; }// город ссылка
    }
}
