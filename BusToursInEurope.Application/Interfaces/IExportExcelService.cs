using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.TourModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IExportExcelService
    {
        Task<byte[]> ExportToursToExcel(List<ShortTourDto> tours);
    }
}
