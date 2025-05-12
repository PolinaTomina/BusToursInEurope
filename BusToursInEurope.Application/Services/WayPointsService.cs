using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.WayPointsModel;
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
                    CityId = wp.CityId,
                    RouteBusId = wp.RouteBusId,
                    HotelId = wp.HotelId,
                }).ToListAsync();

            return wayPoints;
        }
    }
}
