using BusToursInEurope.Application.Models.CityModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface ICities
    {
        Task AddCityAsync(CityDto cityDto);
        Task DeleteCityAsync(int id);
        Task UpdateCityAsync(int id, CityDto cityDto);
    }
}
