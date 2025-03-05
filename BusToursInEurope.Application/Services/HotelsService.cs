using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class HotelsService : IHotels
    {
        private readonly ApplicationContext _context;

        public HotelsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddHotelAsync(HotelDto hotelDto)
        {
            var city = await _context.Cities.FindAsync(hotelDto.CityDtoId);
            if (city == null)
            {
                throw new KeyNotFoundException("Город не найден");
            }

            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Rating = hotelDto.Rating,
                CityId = hotelDto.CityDtoId
            };

            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Отель не найден");
            }
        }

        public async Task UpdateHotelAsync(int id, HotelDto hotelDto)
        {
            var hotel = await _context.Hotels
                .Include(h => h.City) 
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                throw new KeyNotFoundException("Отель не найден");
            }

            hotel.Name = hotelDto.Name;
            hotel.Rating = hotelDto.Rating;

            if (hotelDto.CityDtoId > 0 && hotel.City?.Id != hotelDto.CityDtoId)
            {
                var city = await _context.Cities.FindAsync(hotelDto.CityDtoId);
                if (city == null)
                {
                    throw new KeyNotFoundException("Город не найден");
                }
                hotel.City = city;
            }

            await _context.SaveChangesAsync();
        }
    }
}
