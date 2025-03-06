using BusToursInEurope.Application.Models.DbModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IBuses
    {
        //Task<BusDto> GetAllBusAsync(int id);
        Task AddBusAsync(BusDto busDto);
        Task DeleteBusAsync(int id);
        Task UpdateBusAsync(int id, BusDto busDto);
        Task<List<BusDto>> GetBusesAsync(BusFilter busFilter);
    }
}
