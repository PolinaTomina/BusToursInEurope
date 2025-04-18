using BusToursInEurope.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportExcelController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IExportExcelService _excelExportService;

        public ExportExcelController(IUserService userService, IExportExcelService excelExportService)
        {
            _userService = userService;
            _excelExportService = excelExportService;
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportUsersToExcel()
        {
            var users = await _userService.GetUsersAsync();
            var excelFile = _excelExportService.ExportUsersToExcel(users);

            return File(excelFile,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Users.xlsx");
        }
    }
}

