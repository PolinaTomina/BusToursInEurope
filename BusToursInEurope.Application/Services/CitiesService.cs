using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Database;

namespace BusToursInEurope.Application.Services
{
    public class CitiesService : ICities
    {
        private readonly ApplicationContext _context;

        public CitiesService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddCityAsync(CityDto cityDto)
        {

        }

        public async Task DeleteCityAsync(int id)
        {

        }

        public async Task UpdateCityAsync(int id, CityDto cityDto)
        {

        }
    }
}
