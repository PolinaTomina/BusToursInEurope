using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.AspNetCore.Routing;
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

        public async Task AddBusAsync(BusDto busDto)
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

        public async Task UpdateBusAsync(int busId, BusDto busDto)
        {
            var bus = await _context.Buses.FindAsync(busId);
            if (bus != null)
            {
                bus.Name = busDto.Name;
                bus.NumOfSeats = busDto.NumOfSeats;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Автобус не найден"); 
            }
        }

        public async Task<List<BusDto>> GetBusesAsync(BusFilter busFilter)
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

            var buses = await query.Select(b => new BusDto
            {
                Id = b.Id,
                Name = b.Name,
                NumOfSeats = b.NumOfSeats
            }).ToListAsync();

            return buses;
        }

    }
}
