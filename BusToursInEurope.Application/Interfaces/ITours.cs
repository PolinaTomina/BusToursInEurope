using BusToursInEurope.Application.Models.TourModel;
using Microsoft.AspNetCore.Http;

namespace BusToursInEurope.Application.Interfaces
{
    public interface ITours
    {
        Task<List<ShortTourDto>> GetTopToursAsync(string? userEmail);
        Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter, string? userEmail);
        Task<FullTourDto> GetFullTourAsync(int id, string? userEmail);
        Task AddTourAsync(CreateTourDto createTourDto);
        Task DeleteTourAsync(int id);
        Task UpdateTourAsync(int id, UpdateTourDto updateTourDto);
    }
}