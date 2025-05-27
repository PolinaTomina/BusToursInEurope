using BusToursInEurope.Application.Models.ReservationModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IReservations
    {
        Task AddReservationAsync(CreateReservationDto reservationDto);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int id);
    }
}
