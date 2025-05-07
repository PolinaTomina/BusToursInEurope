using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.WayPointsModel;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/wayPoints")]

    public class WayPointController : ControllerBase
    {
        private readonly IWayPoints _wayPoints;

        public WayPointController(IWayPoints wayPoints)
        {
            _wayPoints = wayPoints;
        }

        [HttpGet("get way points")]
        public async Task<ActionResult<List<WayPointDto>>> GetWayPoints()
        {
            var wp = await _wayPoints.GetWayPointsAsync();
            return Ok(wp);
        }
    }
}
