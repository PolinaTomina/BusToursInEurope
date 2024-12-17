using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Services
{
    public class ShowToursService : IShowTours
    {
        public List<ShortTourDto> GetTopToursAsync()
        {
            // получить список туров из БД: var entities = applicationCntext.Tours
            var tours = new List<ShortTourDto>
            {
                new ShortTourDto { Id = 1, Name = "Paris", Price = 400, StartDate = new DateTime(2005, 12, 1) },
                new ShortTourDto { Id = 2, Name = "Rome",  Price = 500, StartDate= new DateTime(2025, 6, 6) },
                new ShortTourDto { Id = 3, Name = "Berlin", Price = 300, StartDate = new DateTime(2025, 5, 9) }
            };
            return tours;
        }

        public Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter)
        {
            throw new NotImplementedException();
        }
    }
}
