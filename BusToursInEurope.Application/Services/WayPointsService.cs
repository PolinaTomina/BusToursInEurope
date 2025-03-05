using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Models.WayPointsModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;

namespace BusToursInEurope.Application.Services
{
    public class WayPointsService : IWayPoints
    {
        private readonly ApplicationContext _context;

        public WayPointsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddWPAsync(CreateWPDto wp)
        {
            var city = await _context.Cities.FindAsync(wp.CityDtoId);
            if (city == null)
            {
                throw new KeyNotFoundException("Город не найден");
            }

            var routeBus = await _context.Routes.FindAsync(wp.RouteBusDtoId);
            if(routeBus == null)
            {
                throw new KeyNotFoundException("Маршрут автобуса не найден");
            }

            var hotel = await _context.Hotels.FindAsync(wp.HotelDtoId);
            if (hotel == null)
            {
                throw new KeyNotFoundException("Отель не найден");
            }
            var wayPoint = new WayPoint
            {
                NamePlace = wp.NamePlace,
                CityId = wp.CityDtoId,
                RouteBusId = wp.RouteBusDtoId,
                HotelId = wp.HotelDtoId
            };

            await _context.WayPoints.AddAsync(wayPoint);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWPAsync(int id)
        {
            var wayPoint = await _context.WayPoints.FindAsync(id);
            if (wayPoint != null)
            {
                _context.WayPoints.Remove(wayPoint);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Точка маршрута не найдена");
            }
        }

        public async Task UpdateWPAsync(int id, CreateWPDto wayPoint)
        {
            // Ищем существующую точку маршрута в базе
            var wp = await _context.WayPoints.FindAsync(id);
            if (wayPoint == null)
            {
                throw new KeyNotFoundException("Точка маршрута не найдена");
            }

            // Обновляем свойства
            wayPoint.NamePlace = wp.NamePlace;

            // Проверяем, был ли передан новый CityId
            if (wayPoint.CityDtoId > 0 && wayPoint.CityDtoId != wp.CityId)
            {
                var city = await _context.Cities.FindAsync(wayPoint.CityDtoId);
                if (city == null)
                {
                    throw new KeyNotFoundException("Город не найден");
                }
                wp.CityId = wayPoint.CityDtoId;
            }

            // Проверяем, был ли передан новый RouteBusId
            if (wayPoint.RouteBusDtoId > 0 && wayPoint.RouteBusDtoId != wp.RouteBusId)
            {
                var routeBus = await _context.Routes.FindAsync(wayPoint.RouteBusDtoId);
                if (routeBus == null)
                {
                    throw new KeyNotFoundException("Маршрут автобуса не найден");
                }
                wp.RouteBusId = wayPoint.RouteBusDtoId;
            }

            // Проверяем, был ли передан новый HotelId
            if (wayPoint.HotelDtoId > 0 && wayPoint.HotelDtoId != wp.HotelId)
            {
                var hotel = await _context.Hotels.FindAsync(wayPoint.HotelDtoId);
                if (hotel == null)
                {
                    throw new KeyNotFoundException("Отель не найден");
                }
                wp.HotelId = wayPoint.HotelDtoId;
            }

            // Сохраняем изменения в БД
            await _context.SaveChangesAsync();
        }
    }
}
