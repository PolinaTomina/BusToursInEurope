using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.BusModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class BusesService : IBuses
    {
        private readonly ApplicationContext _context;

        public BusesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddBusAsync(CreateBusDto busDto)
        {
            var bus = new Bus
            {
                Name = busDto.Name,
                NumOfSeats = busDto.NumOfSeats
            };

            await _context.Buses.AddAsync(bus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusAsync(int busId)
        {
            var bus = await _context.Buses.FindAsync(busId);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Автобус не найден");
            }
        }

        public async Task UpdateBusAsync(int busId, UpdateBusDto busDto)
        {
            var bus = await _context.Buses.FindAsync(busId);
            if (bus != null)
            {
                if (!string.IsNullOrEmpty(busDto.Name)) bus.Name = busDto.Name;
                if (busDto.NumOfSeats.HasValue) bus.NumOfSeats = busDto.NumOfSeats.Value;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Автобус не найден"); 
            }
        }

        public async Task<List<ShowBusDto>> GetBusesAsync(BusFilter busFilter)
        {
            var query = _context.Buses.AsQueryable();

            if (!string.IsNullOrEmpty(busFilter.Name))
            {
                query = query.Where(b => b.Name.Contains(busFilter.Name));
            }
            if (busFilter.MinSeats.HasValue)
            {
                query = query.Where(b => b.NumOfSeats >= busFilter.MinSeats.Value);
            }
            if (busFilter.MaxSeats.HasValue)
            {
                query = query.Where(b => b.NumOfSeats <= busFilter.MaxSeats.Value);
            }

            if (!string.IsNullOrEmpty(busFilter.SortBy))
            {
                query = busFilter.SortBy.ToLower() switch
                {
                    "name" => busFilter.IsDescending ? query.OrderByDescending(b => b.Name) : query.OrderBy(b => b.Name),
                    "numofseats" => busFilter.IsDescending ? query.OrderByDescending(b => b.NumOfSeats) : query.OrderBy(b => b.NumOfSeats),
                    _ => query.OrderBy(b => b.Id)
                };
            }

            var buses = await query.Select(b => new ShowBusDto
            {
                Id = b.Id,
                Name = b.Name,
                NumOfSeats = b.NumOfSeats
            }).ToListAsync();

            return buses;
        }

    }
}
