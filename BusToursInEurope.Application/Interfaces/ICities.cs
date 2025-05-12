using BusToursInEurope.Application.Models.CityModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface ICities
    {
        Task AddCityAsync(CreateCityDto cityDto);
        Task DeleteCityAsync(int id);
        Task UpdateCityAsync(int id, CreateCityDto cityDto);
        Task<List<CreateCityDto>> GetCitiesAsync(CityFilter cityFilter);
    }
}
