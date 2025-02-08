using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.RoutesBusModels;
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
            var route = _context.Routes.SingleOrDefaultAsync(r => r.Id == id);

            if (route == null)
            {
                throw new ApplicationException($"Маршрут с id {id} не найден");
            }

            _context.Remove(route);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRouteBusAsync(UpdateRouteBusDto request)
        {
            var route = await _context.Routes.SingleOrDefaultAsync(r => r.Id == request.Id);

            if (route == null)
            {
                throw new ApplicationException($"Маршрут с id {request.Id} не найден");
            }

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
