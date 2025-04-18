using BusToursInEurope.Application.Models.ExelModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IExportExcelService
    {
        byte[] ExportUsersToExcel(List<ExportExcelUserDto> users);
    }
}
