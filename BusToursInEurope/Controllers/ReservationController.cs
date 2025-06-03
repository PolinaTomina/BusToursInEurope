using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReservationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Authorize]
    [Route("reservations")]

    public class ReservationController : ControllerBase
    {
        private readonly IReservations _reservationsService;
        private readonly IExportExcelService _exportExcelService;

        public ReservationController(IReservations reservationsService, IExportExcelService exportExcelService)
        {
            _reservationsService = reservationsService;
            _exportExcelService = exportExcelService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] CreateReservationDto reservationDto)
        {
            var userEmail = User.Claims.FirstOrDefault();

            await _reservationsService.AddReservationAsync(reservationDto, userEmail?.Value);

            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("update-payment")]
        public async Task<IActionResult> UpdatePaymentStatus([FromBody]UpdatePaymentStatusDto request)
        {
            await _reservationsService.UpdatePaymentStatusAsync(request);

            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationsService.DeleteReservationAsync(id);
            return NoContent();
        }

        [HttpGet("ForUser")]
        public async Task<IActionResult> GetUserReservations()
        {
            var userEmail = User.Claims.FirstOrDefault();

            var reservations = await _reservationsService.GetUserReservationsAsync(userEmail.Value);

            return Ok(reservations);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllReservations()
        {
            var userEmail = User.Claims.FirstOrDefault();

            var reservations = await _reservationsService.GetAllReservationsAsync();

            return Ok(reservations);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _reservationsService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("reservations_export")]
        public async Task<IActionResult> ExportReservations()
        {
            var reservationsExport = await _reservationsService.GetReservationsForExportAsync();
            var fileContents = await _exportExcelService.ExportReservationsToExcel(reservationsExport);

            return File(fileContents,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Reservations.xlsx");
        }
    }
}
