using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
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

        public async Task AddWPAsync(WayPointDto wayPoint)
        {

        }

        public async Task DeleteWPAsync(int id)
        {

        }

        public async Task UpdateWPAsync(int id, WayPointDto wayPoint)
        {

        }
    }
}
