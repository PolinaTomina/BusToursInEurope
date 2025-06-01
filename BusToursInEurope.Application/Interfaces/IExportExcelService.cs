using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IExportExcelService
    {
        Task<byte[]> ExportToursToExcel(List<ShortTourDto> tours);
        Task<byte[]> ExportReservationsToExcel(List<ReservationExportDto> reservations);
    }
}
