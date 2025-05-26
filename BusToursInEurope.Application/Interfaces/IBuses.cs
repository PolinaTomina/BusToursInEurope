using BusToursInEurope.Application.Models.BusModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IBuses
    {
        //Task<BusDto> GetAllBusAsync(int id);
        Task AddBusAsync(CreateBusDto busDto);
        Task DeleteBusAsync(int id);
        Task UpdateBusAsync(int id, UpdateBusDto busDto);
        Task<List<CreateBusDto>> GetBusesAsync(BusFilter busFilter);
    }
}
