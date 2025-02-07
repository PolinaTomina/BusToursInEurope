using BusToursInEurope.Application.Models.DbModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IRouteBuses
    {
        Task AddRouteBusAsync(RouteBusDto routeBusDto);
        Task DeleteRouteBusAsync(int id);
        Task UpdateRouteBusAsync(int id, RouteBusDto routeBusDto);
    }
}
