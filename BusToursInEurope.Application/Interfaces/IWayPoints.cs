using BusToursInEurope.Application.Models.WayPointsModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IWayPoints
    {
        Task<List<ShowWayPointsDto>> GetWayPointsAsync();
    }
}
