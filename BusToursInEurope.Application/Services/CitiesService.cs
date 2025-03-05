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

        public async Task AddCityAsync(CityDto cityDto)
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

        public async Task UpdateCityAsync(int id, CityDto cityDto)
        {
            var city = await _context.Cities
            .Include(c => c.Hotel)
            .Include(c => c.WayPoints)
            .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                throw new KeyNotFoundException($"Город не найден");
            }

            // Обновляем свойства
            city.Name = cityDto.Name;
            city.Country = cityDto.Country;
            city.Visa = cityDto.Visa;

            // Обновляем отели (если был передан новый HotelId)
            if (cityDto.HotelIds.Any())
            {
                var hotels = await _context.Hotels
                    .Where(h => cityDto.HotelIds.Contains(h.Id))
                    .ToListAsync();

                city.Hotel = hotels; // Привязываем найденные отели к городу
            }

            // Обновляем точки маршрута (если были переданы новые WayPointIds)
            if (cityDto.WayPointIds.Any())
            {
                var wayPoints = await _context.WayPoints
                    .Where(wp => cityDto.WayPointIds.Contains(wp.Id))
                    .ToListAsync();

                city.WayPoints = wayPoints; // Привязываем найденные точки маршрута
            }
            await _context.SaveChangesAsync();

        }
    }
}
