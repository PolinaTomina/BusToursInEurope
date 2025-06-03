using NPOI.XSSF.UserModel;
using BusToursInEurope.Application.Interfaces;
using NPOI.SS.UserModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application
{
    public class ExportExcelService : IExportExcelService
    {
        public async Task<byte[]> ExportToursToExcel(List<ShortTourDto> tours)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Top Tours");

            IRow headerRow = sheet.CreateRow(0);
            string[] headers = { "Идентификационный номер", "Название", "Цена", "Дата начала", "Дата окончания", "Количество бронирований", "Рейтинг" };

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
                row.CreateCell(5).SetCellValue(tour.ReservationCount);
                row.CreateCell(6).SetCellValue((double)tour.Rating);
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }

        public async Task<byte[]> ExportReservationsToExcel(List<ReservationExportDto> reservations)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Reservations");

            IRow headerRow = sheet.CreateRow(0);
            string[] headers = { "Идентификационный номер бронирования", "Дата бронирования", "Дата оплаты", "Срок оплаты",
                             "Количество забронированных мест", "Почта пользователя", "Идентификационный номер тура", "Название тура" };

            for (int i = 0; i < headers.Length; i++)
            {
                headerRow.CreateCell(i).SetCellValue(headers[i]);
            }

            for (int i = 0; i < reservations.Count; i++)
            {
                var res = reservations[i];
                IRow row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(res.ReservationId);
                row.CreateCell(1).SetCellValue(res.Date.ToString("yyyy-MM-dd"));
                row.CreateCell(2).SetCellValue(res.PaymentDate?.ToString("yyyy-MM-dd") ?? "Not Paid");
                row.CreateCell(3).SetCellValue(res.PaymentDeadline.ToString("yyyy-MM-dd"));
                row.CreateCell(4).SetCellValue(res.NumOfSeats);
                row.CreateCell(5).SetCellValue(res.UserEmail.Email);
                row.CreateCell(6).SetCellValue(res.ExportExcelReservationTour.TourId);
                row.CreateCell(7).SetCellValue(res.ExportExcelReservationTour.TourName);
            }

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }
    }
}
