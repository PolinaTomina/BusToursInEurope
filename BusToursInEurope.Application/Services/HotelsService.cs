using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Database;

namespace BusToursInEurope.Application.Services
{
    public class HotelsService : IHotels
    {
        private readonly ApplicationContext _context;

        public HotelsService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddHotelAsync(HotelDto hotelDto)
        {

        }

        public async Task DeleteHotelAsync(int id)
        {

        }

        public async Task UpdateHotelAsync(int id, HotelDto hotelDto)
        {

        }
    }
}
