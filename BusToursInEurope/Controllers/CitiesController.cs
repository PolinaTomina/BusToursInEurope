using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Services;
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
        public async Task<ActionResult> AddCity(CityDto cityDto)
        {
            await _cities.AddCityAsync(cityDto);
            return StatusCode(201);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteCity(int id)
        {
            await _cities.DeleteCityAsync(id);
            return StatusCode(201);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateCity(int id, [FromBody] CityDto cityDto)
        {
            await _cities.UpdateCityAsync(id, cityDto);
            return StatusCode(201);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetCities([FromQuery] CityFilter cityFilter)
        {
            var cities = await _cities.GetCitiesAsync(cityFilter);
            return Ok(cities);
        }
    }
}
