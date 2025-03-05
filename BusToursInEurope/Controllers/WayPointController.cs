using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.WayPointsModel;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/wayPoints")]

    public class WayPointController : ControllerBase
    {
        private readonly IWayPoints _wp;

        public WayPointController(IWayPoints wp)
        {
            _wp = wp;
        }

        [HttpPost]
        public async Task<ActionResult> AddWP(CreateWPDto wayPoint)
        {
            await _wp.AddWPAsync(wayPoint);
            return StatusCode(201);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteWP(int id)
        {
            await _wp.DeleteWPAsync(id);
            return StatusCode(201);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateWP(int id, [FromBody] CreateWPDto wayPoint)
        {
            await _wp.UpdateWPAsync(id, wayPoint);
            return StatusCode(201);
        }
    }
}
