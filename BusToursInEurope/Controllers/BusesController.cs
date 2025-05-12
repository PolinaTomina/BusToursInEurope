using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/buses")]

    public class BusesController : ControllerBase
    {
        private readonly IBuses _crudBuses;

        public BusesController(IBuses crudBuses)
        {
            _crudBuses = crudBuses;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddBus(BusDto busDto)
        {
            await _crudBuses.AddBusAsync(busDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteBus(int id)
        {
            await _crudBuses.DeleteBusAsync(id);
            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("id")]
        public async Task<ActionResult> UpdateBus(int id, [FromBody] BusDto busDto)
        {
            await _crudBuses.UpdateBusAsync(id, busDto);
            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("filters")]
        public async Task<IActionResult> GetBuses([FromQuery] BusFilter busFilter)
        {
            var buses = await _crudBuses.GetBusesAsync(busFilter);
            return Ok(buses);
        }

    }
}
