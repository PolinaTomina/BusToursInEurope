using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IShowTours
    {
        Task<List<ShortTourDto>> GetTopToursAsync();
        Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter);
        Task<FullTourDto> GetFullTourAsync(int id);
        Task AddTourAsync(CreateTourDto createTourDto);
        Task DeleteTourAsync(int id);
        Task UpdateTourAsync(int id, UpdateTourDto updateTourDto);
    }
}