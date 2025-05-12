using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReservationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("reservations")]

    public class ReservationController : ControllerBase
    {
        private readonly IReservations _reservationsService;

        public ReservationController(IReservations reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpPost]
        public async Task<ActionResult> AddReservation([FromBody] CreateReservationDto reservationDto)
        {
            await _reservationsService.AddReservationAsync(reservationDto);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            await _reservationsService.DeleteReservationAsync(id);
            return NoContent();
        }

        // Нужен ли метод обновления брони?
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] CreateReservationDto reservationDto)
        {
            await _reservationsService.UpdateReservationAsync(id, reservationDto);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations()
        {
            var reservations = await _reservationsService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservationById(int id)
        {
            var reservation = await _reservationsService.GetReservationByIdAsync(id);
            return Ok(reservation);
        }
    }
}
