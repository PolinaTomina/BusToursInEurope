using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddCity(CityDto cityDto)
        {
            await _cities.AddCityAsync(cityDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            await _cities.DeleteCityAsync(id);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("id")]
        public async Task<ActionResult> UpdateCity(int id, [FromBody] CityDto cityDto)
        {
            await _cities.UpdateCityAsync(id, cityDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("filters")]
        public async Task<IActionResult> GetCities([FromQuery] CityFilter cityFilter)
        {
            var cities = await _cities.GetCitiesAsync(cityFilter);
            return Ok(cities);
        }
    }
}
