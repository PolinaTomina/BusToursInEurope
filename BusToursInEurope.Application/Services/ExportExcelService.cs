using NPOI.XSSF.UserModel;
using BusToursInEurope.Application.Interfaces;
using NPOI.SS.UserModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.ReservationModel;

namespace BusToursInEurope.Application
{
    public class ExportExcelService : IExportExcelService
    {
        public async Task<byte[]> ExportToursToExcel(List<ShortTourDto> tours)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Top Tours");

            IRow headerRow = sheet.CreateRow(0);
            string[] headers = { "Id", "Name", "Price", "Start Date", "End Date" };

            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }

            for (int i = 0; i < tours.Count; i++)
            {
                var tour = tours[i];
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(tour.Id);
                row.CreateCell(1).SetCellValue(tour.Name);
                row.CreateCell(2).SetCellValue((double)tour.Price);
                row.CreateCell(3).SetCellValue(tour.StartDate.ToString("yyyy-MM-dd"));
                row.CreateCell(4).SetCellValue(tour.EndDate.ToString("yyyy-MM-dd"));
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }
    }
}
