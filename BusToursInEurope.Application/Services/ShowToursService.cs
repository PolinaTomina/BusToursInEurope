using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;
using BusToursInEurope.Core.Entites;
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

        public async Task AddTourAsync(CreateTourDto createTourDto)
        {
            var tour = new Tour
            {
                Name = createTourDto.Name,
                Price = createTourDto.Price,
                StartDate = createTourDto.StartDate,
                EndDate = createTourDto.EndDate,
                Route = createTourDto.Route,
                NumOfSeats = createTourDto.NumOfSeats,
                Description = createTourDto.Description,
            };

            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();
        }
    }
}
