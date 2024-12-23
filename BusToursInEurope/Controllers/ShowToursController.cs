using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/tours")]
    public class ShowToursController : ControllerBase
    {
        private readonly IShowTours _showTours;
        private readonly ILogger<ShowToursController> _logger;

        public ShowToursController(IShowTours showTours, ILogger<ShowToursController> logger)
        {
            _showTours = showTours;
            _logger = logger;
        }

        [HttpGet("top")]
        public ActionResult<List<ShortTourDto>> GetTopTours()
        {
            var topTours = _showTours.GetTopToursAsync();
            return Ok(topTours);
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
            return StatusCode(201);
        }
    }
}
