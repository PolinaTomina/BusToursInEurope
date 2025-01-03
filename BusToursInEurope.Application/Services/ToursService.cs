using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BusToursInEurope.Application.Services
{
    public class ToursService : ITours
    {
        private readonly ApplicationContext _context;

        public ToursService(ApplicationContext context)
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

        public async Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter)
        {
            var query = _context.Tours.AsQueryable();

            if (!string.IsNullOrEmpty(toursFilter.Country))
            {
                query = query.Where(t => t.Name.Contains(toursFilter.Country));
            }
            if (toursFilter.MinPrice.HasValue)
            {
                query = query.Where(t => t.Price >= toursFilter.MinPrice.Value);
            }
            if (toursFilter.MaxPrice.HasValue)
            {
                query = query.Where(t => t.Price <= toursFilter.MaxPrice.Value);
            }
            if (toursFilter.StartDate.HasValue) 
            { 
                query = query.Where(t => t.StartDate >= toursFilter.StartDate.Value); 
            }
            //if (toursFilter.EndDate.HasValue) 
            //{ 
            //    query = query.Where(t => t.EndDate <= toursFilter.EndDate.Value); 
            //}

            var filteredTours = await query
                .Select(t => new ShortTourDto 
                { 
                    Id = t.Id, 
                    Name = t.Name, 
                    Price = t.Price, 
                    StartDate = t.StartDate, 
                }).ToListAsync(); 
            
            return filteredTours;

        }

        public async Task<FullTourDto> GetFullTourAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
                return null;

            return new FullTourDto
            {
                Name = tour.Name,
                Price = tour.Price,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                Route = tour.Route,
                NumOfSeats = tour.NumOfSeats,
                Description = tour.Description
            };
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

        public async Task DeleteTourAsync(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTourAsync(int tourId, UpdateTourDto updateTourDto)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if(tour != null)
            {
                tour.Name = updateTourDto.Name;
                tour.Price = updateTourDto.Price;
                tour.StartDate = updateTourDto.StartDate;
                tour.EndDate = updateTourDto.EndDate;
                tour.Route = updateTourDto.Route;
                tour.NumOfSeats = updateTourDto.NumOfSeats;
                tour.Description = updateTourDto.Description;

                await _context.SaveChangesAsync();
            }
        }
    }
}
