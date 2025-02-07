using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.AspNetCore.Routing;

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
    }
}
