using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Interfaces
{
    /// <summary>
    /// при входе на страницу показывает топ туры
    /// </summary>
    public interface IShowTours
    {
        List<ShortTourDto> GetTopToursAsync();

        Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter);
    }
}