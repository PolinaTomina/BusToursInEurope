using BusToursInEurope.Application.Models.RoutesBusModels;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IRouteBusesService
    {
        Task AddRouteBusAsync(CreateRouteBusDto request);
        Task DeleteRouteBusAsync(int id);
        Task UpdateRouteBusAsync(UpdateRouteBusDto request);
        Task<List<RouteBusDto>> GetAll();
    }
}
