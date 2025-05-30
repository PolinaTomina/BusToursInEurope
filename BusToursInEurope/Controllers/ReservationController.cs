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

        public ReservationController(IReservations reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] CreateReservationDto reservationDto)
        {
            var userEmail = User.Claims.FirstOrDefault();

            await _reservationsService.AddReservationAsync(reservationDto, userEmail?.Value);

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationsService.DeleteReservationAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
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
    }
}
