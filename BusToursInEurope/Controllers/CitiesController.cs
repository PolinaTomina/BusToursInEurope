using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/cities")]

    public class CitiesController : ControllerBase
    {
        private readonly ICities _cities;

        public CitiesController(ICities cities)
        {
            _cities = cities;
        }

        [HttpPost]
        public async Task<ActionResult> AddBus(BusDto busDto)
        {
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteBus(int id)
        {
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateBus(int id, [FromBody] BusDto busDto)
        {
            return Ok();
        }
    }
}
