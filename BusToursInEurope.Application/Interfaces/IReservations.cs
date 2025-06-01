using BusToursInEurope.Application.Models.ReservationModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IReservations
    {
        Task AddReservationAsync(CreateReservationDto reservationDto, string userEmail);
        Task DeleteReservationAsync(int id);
        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string userEmail);
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int id);
        Task UpdatePaymentStatusAsync(UpdatePaymentStatusDto request);
        Task<List<ReservationExportDto>> GetReservationsForExportAsync();
    }
}
