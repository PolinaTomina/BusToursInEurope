using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
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
        public async Task<ActionResult> AddBus(CityDto cityDto)
        {
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteBus(int id)
        {
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateBus(int id, [FromBody] CityDto cityDto)
        {
            return Ok();
        }
    }
}
