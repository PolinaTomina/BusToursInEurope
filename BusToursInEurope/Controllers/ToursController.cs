using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/tours")]
    public class ToursController : ControllerBase
    {
        private readonly ITours _showTours;
        private readonly ILogger<ToursController> _logger;

        public ToursController(ITours showTours, ILogger<ToursController> logger)
        {
            _showTours = showTours;
            _logger = logger;
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<ShortTourDto>>> GetTopTours()
        {
            var topTours = await _showTours.GetTopToursAsync();
            return Ok(topTours);
        }

        [HttpGet] 
        public async Task<ActionResult<List<ShortTourDto>>> GetTours([FromQuery] ToursFilter toursFilter) 
        { 
            var tours = await _showTours.GetToursAsync(toursFilter); 
            return Ok(tours);
        }

        [HttpGet("id")]
        public async Task<ActionResult<FullTourDto>> GetFullTour(int id)
        {
            var tour = await _showTours.GetFullTourAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }

        [HttpPost]
        public async Task<ActionResult> AddTour(CreateTourDto createTourDto)
        {
            await _showTours.AddTourAsync(createTourDto);
            return StatusCode(201);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteTour(int id)
        {
            await _showTours.DeleteTourAsync(id);
            return NoContent();
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateTour(int id, [FromBody] UpdateTourDto updateTourDto)
        {
            await _showTours.UpdateTourAsync(id, updateTourDto);
            return NoContent();
        }
    }
}
