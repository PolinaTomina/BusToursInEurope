using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Interfaces
{
    /// <summary>
    /// при входе на страницу показывает топ туры
    /// </summary>
    public interface IShowTours
    {
        Task<List<Tour>> GetTopToursAsync();

        Task<List<Tour>> GetToursAsync(ToursFilter toursFilter);
    }
}