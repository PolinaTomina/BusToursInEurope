using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class ShowToursService : IShowTours
    {
        private readonly ApplicationContext _context;

        public ShowToursService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ShortTourDto>> GetTopToursAsync()
        {
            // получить список туров из БД: var entities = applicationCntext.Tours
            var tours = await _context.Tours.
                Select(t => new ShortTourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    StartDate = t.StartDate,
                }).ToListAsync();

            return tours;
        }

        public Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter)
        {
            throw new NotImplementedException();
        }
    }
}
