using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.WayPointsModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class WayPointsService : IWayPoints
    {
        private readonly ApplicationContext _context;

        public WayPointsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ShowWayPointsDto>> GetWayPointsAsync()
        {
            var wayPoints = await _context.WayPoints.
                Select(wp => new ShowWayPointsDto
                {
                    Id = wp.Id,
                    NamePlace = wp.NamePlace,
                    CityDtoId = wp.CityId,
                    RouteBusDtoId = wp.RouteBusId,
                    HotelDtoId = wp.HotelId,
                }).ToListAsync();

            return wayPoints;
        }
    }
}
