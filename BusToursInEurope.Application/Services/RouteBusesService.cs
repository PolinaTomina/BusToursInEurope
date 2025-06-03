using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.RoutesBusModels;
using BusToursInEurope.Application.Models.WayPointsModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class RouteBusesService : IRouteBusesService
    {
        private readonly ApplicationContext _context;

        public RouteBusesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddRouteBusAsync(CreateRouteBusDto request)
        {
            if (request.WayPoints.Count < 2)
            {
                throw new ApplicationException("Маршрут должен содержать 2 или более точки остановки");
            }

            var route = new RouteBus
            {
                Name = request.Name,
                Distance = request.Distance,
                WayPoints = request.WayPoints
                    .Select(w => new WayPoint
                    {
                        NamePlace = w.NamePlace,
                        CityId = w.CityId,
                        HotelId = w.HotelId
                    }).ToList()
            };

            await _context.AddAsync(route);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRouteBusAsync(int id)
        {
            var route = await _context.RoutesBuses.SingleOrDefaultAsync(r => r.Id == id);

            if (route == null)
            {
                throw new ApplicationException($"Маршрут с id {id} не найден");
            }

            _context.Remove(route);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RouteBusDto>> GetAll()
        {
            var routes = await _context.RoutesBuses
                .Select(x => new RouteBusDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Distance = x.Distance,
                }).ToListAsync();

            foreach (var item in routes)
            {
                item.WayPointsDto = await _context.WayPoints
                        .Select(w => new WayPointDto
                        {
                            Id = w.Id,
                            CityId = w.CityId,
                            HotelId = w.HotelId,
                            NamePlace = w.NamePlace,
                        }).ToListAsync();
            }

            return routes;
        }

        public async Task UpdateRouteBusAsync(UpdateRouteBusDto request)
        {
            var route = await _context.RoutesBuses
                .Include(r => r.WayPoints)
                .SingleOrDefaultAsync(r => r.Id == request.Id);

            if (route == null)
            {
                throw new ApplicationException($"Маршрут с id {request.Id} не найден");
            }

            _context.RemoveRange(route.WayPoints);
            await _context.SaveChangesAsync();

            route.WayPoints = new List<WayPoint>();

            route.Name = request.Name;
            route.Distance = request.Distance;
            route.WayPoints = request.WayPoints
                    .Select(w => new WayPoint
                    {
                        NamePlace = w.NamePlace,
                        CityId = w.CityId,
                        HotelId = w.HotelId
                    }).ToList();

            await _context.SaveChangesAsync();
        }
    }
}
