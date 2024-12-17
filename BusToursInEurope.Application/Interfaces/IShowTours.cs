using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IShowTours
    {
        List<ShortTourDto> GetTopToursAsync();
        Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter);
    }
}