using System.Linq;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using Microsoft.EntityFrameworkCore;
using BusToursInEurope.Database;
using BusToursInEurope.Application.Models.ExelModel;
using BusToursInEurope.Application.Interfaces;

namespace BusToursInEurope.Application
{
    public class ExportExcelService : IExportExcelService
    {
        public byte[] ExportUsersToExcel(List<ExportExcelUserDto> users)
        {
            using var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Users");

            // Заголовки колонок
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ID");
            headerRow.CreateCell(1).SetCellValue("Full Name");
            headerRow.CreateCell(2).SetCellValue("Email");
            headerRow.CreateCell(3).SetCellValue("Phone Number");

            // Заполняем данные
            for (int i = 0; i < users.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(users[i].Id);
                row.CreateCell(1).SetCellValue(users[i].FullName ?? "N/A");
                row.CreateCell(2).SetCellValue(users[i].Email);
                row.CreateCell(3).SetCellValue(users[i].NumPhone);
            }

            // Записываем в поток
            using var stream = new MemoryStream();
            workbook.Write(stream);
            return stream.ToArray();
        }
    }
}
