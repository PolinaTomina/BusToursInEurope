using BusToursInEurope.Application.Models;

namespace BusToursInEurope.Application.Services
{
    /// <summary>
    /// при входе на страницу показывает топ туры
    /// </summary>
    public interface IShowToursService
    {
        Task<List<Tour>> GetTopToursAsync();

        Task<List<Tour>> GetToursAsync(ToursFilter toursFilter);
    }
}
