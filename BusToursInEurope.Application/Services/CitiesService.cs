using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class CitiesService : ICities
    {
        private readonly ApplicationContext _context;

        public CitiesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddCityAsync(CreateCityDto cityDto)
        {
            var city = new City
            {
                Name = cityDto.Name,
                Country = cityDto.Country,
                Visa = cityDto.Visa
            };

            if (cityDto.HotelIds.Any())
            {
                var hotels = await _context.Hotels
                    .Where(h => cityDto.HotelIds.Contains(h.Id))
                    .ToListAsync();
                city.Hotel = hotels;
            }

            if (cityDto.WayPointIds.Any())
            {
                var wayPoints = await _context.WayPoints
                    .Where(w => cityDto.WayPointIds.Contains(w.Id))
                    .ToListAsync();
                city.WayPoints = wayPoints;
            }

            await _context.Cities.AddAsync(city);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Город не найден");
            }
        }

        public async Task UpdateCityAsync(int id, CreateCityDto cityDto)
        {
            var city = await _context.Cities
            .Include(c => c.Hotel)
            .Include(c => c.WayPoints)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                throw new KeyNotFoundException($"Город не найден");
            }

            city.Name = cityDto.Name;
            city.Country = cityDto.Country;
            city.Visa = cityDto.Visa;

            if (cityDto.HotelIds.Any())
            {
                var hotels = await _context.Hotels
                    .Where(h => cityDto.HotelIds.Contains(h.Id))
                    .ToListAsync();

                city.Hotel = hotels; 
            }

            if (cityDto.WayPointIds.Any())
            {
                var wayPoints = await _context.WayPoints
                    .Where(wp => cityDto.WayPointIds.Contains(wp.Id))
                    .ToListAsync();

                city.WayPoints = wayPoints; 
            }
            await _context.SaveChangesAsync();
        }
        public async Task<List<CreateCityDto>> GetCitiesAsync(CityFilter cityFilter)
        {
            var query = _context.Cities.AsQueryable();

            // Фильтрация
            if (!string.IsNullOrEmpty(cityFilter.Name))
            {
                query = query.Where(c => c.Name.Contains(cityFilter.Name));
            }
            if (!string.IsNullOrEmpty(cityFilter.Country))
            {
                query = query.Where(c => c.Country.Contains(cityFilter.Country));
            }
            if (cityFilter.VisaRequired.HasValue)
            {
                query = query.Where(c => c.Visa == cityFilter.VisaRequired.Value);
            }

            // **Сортировка**
            if (!string.IsNullOrEmpty(cityFilter.SortBy))
            {
                query = cityFilter.SortBy.ToLower() switch
                {
                    "name" => cityFilter.IsDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name),
                    "country" => cityFilter.IsDescending ? query.OrderByDescending(c => c.Country) : query.OrderBy(c => c.Country),
                    _ => query.OrderBy(c => c.Id) // Если поле сортировки не задано, сортируем по ID
                };
            }

            // Преобразуем в DTO
            var cities = await query.Select(c => new CreateCityDto
            {
                Name = c.Name,
                Country = c.Country,
                Visa = c.Visa
            }).ToListAsync();

            return cities;
        }
    }
}
