using BusToursInEurope.Application.Models.CityModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface ICities
    {
        Task AddCityAsync(CreateCityDto cityDto);
        Task DeleteCityAsync(int id);
        Task UpdateCityAsync(int id, UpdateCityDto cityDto);
        Task<List<ShowCityDto>> GetCitiesAsync(CityFilter cityFilter);
    }
}
