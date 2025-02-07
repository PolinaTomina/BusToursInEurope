using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Models.TourModel;
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

        [HttpPost]
        public async Task<ActionResult> AddBus(BusDto busDto)
        {
            await _crudBuses.AddBusAsync(busDto);
            return StatusCode(201);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteBus(int id)
        {
            await _crudBuses.DeleteBusAsync(id);
            return NoContent();
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateBus(int id, [FromBody] BusDto busDto)
        {
            await _crudBuses.UpdateBusAsync(id, busDto);
            return NoContent();
        }
    }
}
