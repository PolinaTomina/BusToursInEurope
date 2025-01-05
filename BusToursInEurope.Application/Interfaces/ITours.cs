using BusToursInEurope.Application.Models.TourModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface ITours
    {
        Task<List<ShortTourDto>> GetTopToursAsync();
        Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter);
        Task<FullTourDto> GetFullTourAsync(int id);
        Task AddTourAsync(CreateTourDto createTourDto);
        Task DeleteTourAsync(int id);
        Task UpdateTourAsync(int id, UpdateTourDto updateTourDto);
    }
}