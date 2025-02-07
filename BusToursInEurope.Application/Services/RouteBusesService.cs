using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CrudModel;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;

namespace BusToursInEurope.Application.Services
{
    public class RouteBusesService : IRouteBuses
    {
        private readonly ApplicationContext _context;

        public RouteBusesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddRouteBusAsync(CrudRouteBusDto crudRouteBusDto)
        {
            var wayPoint = await _context.WayPoints.FindAsync(crudRouteBusDto.WayPointDto);
            if (wayPoint == null)
            {
                throw new ArgumentException($"Маршрут автобуса с ID {crudRouteBusDto.WayPointDto} не найден");
            }

            var routeBus = new RouteBus
            {
                Distance = crudRouteBusDto.Distance,
                WayPoints = wayPoint
            };

            await _context.Routes.AddAsync(routeBus);
            await _context.SaveChangesAsync();
        }

        public async Task ADeleteRouteBusAsync(int id)
        {

        }

        public async Task UpdateRouteBusAsync(int id, CrudRouteBusDto crudRouteBusDto)
        {

        }
    }
}
