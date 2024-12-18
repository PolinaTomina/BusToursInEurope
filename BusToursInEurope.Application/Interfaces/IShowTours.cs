using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IShowTours
    {
        Task<List<ShortTourDto>> GetTopToursAsync();
        Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter);
    }
}