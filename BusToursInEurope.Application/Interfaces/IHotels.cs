using BusToursInEurope.Application.Models.HotelModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IHotels
    {
        Task AddHotelAsync(CreateHotelDto hotelDto);
        Task DeleteHotelAsync(int id);
        Task UpdateHotelAsync(int id, UpdateHotelDto hotelDto);
        Task<List<ShowHotelDto>> GetHotelsAsync(HotelFilter hotelFilter);
    }
}
