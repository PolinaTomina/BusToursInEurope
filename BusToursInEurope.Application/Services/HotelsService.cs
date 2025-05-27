using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.HotelModel;
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

        public async Task AddHotelAsync(CreateHotelDto hotelDto)
        {
            var city = await _context.Cities.FindAsync(hotelDto.CityId);
            if (city == null)
            {
                throw new KeyNotFoundException("Город не найден");
            }

            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                Rating = hotelDto.Rating,
                CityId = hotelDto.CityId
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

        public async Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto)
        {
            var hotel = await _context.Hotels
                .Include(h => h.City) 
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                throw new KeyNotFoundException("Отель не найден");
            }

            if (!string.IsNullOrEmpty(hotelDto.Name)) hotelDto.Name = hotelDto.Name;
            if (hotelDto.Rating.HasValue) hotelDto.Rating = hotelDto.Rating.Value;

            if (hotelDto.CityId > 0 && hotel.City?.Id != hotelDto.CityId)
            {
                var city = await _context.Cities.FindAsync(hotelDto.CityId);
                if (city == null)
                {
                    throw new KeyNotFoundException("Город не найден");
                }
                hotel.City = city;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<ShowHotelDto>> GetHotelsAsync(HotelFilter hotelFilter)
        {
            var query = _context.Hotels.AsQueryable();

            if (!string.IsNullOrEmpty(hotelFilter.Name))
            {
                query = query.Where(h => h.Name.Contains(hotelFilter.Name));
            }
            if (hotelFilter.MinRating.HasValue)
            {
                query = query.Where(h => h.Rating >= hotelFilter.MinRating.Value);
            }
            if (hotelFilter.MaxRating.HasValue)
            {
                query = query.Where(h => h.Rating <= hotelFilter.MaxRating.Value);
            }
            if (hotelFilter.CityId.HasValue)
            {
                query = query.Where(h => h.CityId == hotelFilter.CityId);
            }

            return await query.Select(h => new ShowHotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Rating = h.Rating,
                CityId = h.CityId
            }).ToListAsync();
        }

    }
}
