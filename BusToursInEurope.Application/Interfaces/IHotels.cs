using BusToursInEurope.Application.Models.HotelModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IHotels
    {
        Task AddHotelAsync(HotelDto hotelDto);
        Task DeleteHotelAsync(int id);
        Task UpdateHotelAsync(int id, HotelDto hotelDto);
    }
}
