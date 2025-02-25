using BusToursInEurope.Application.Models.WayPointsModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IWayPoints
    {
        Task AddWPAsync(WayPointDto wayPoint);
        Task DeleteWPAsync(int id);
        Task UpdateWPAsync(int id, WayPointDto wayPoint);
    }
}
