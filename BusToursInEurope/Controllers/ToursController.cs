using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.TourModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using BusToursInEurope.Database;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/tours")]
    public class ToursController : ControllerBase
    {
        private readonly ITours _showTours;
        private readonly ApplicationContext _context;

        public ToursController(ITours showTours, ApplicationContext context)
        {
            _showTours = showTours;
            _context = context;
        }

        [HttpGet("top")]
        public async Task<ActionResult<List<ShortTourDto>>> GetTopTours()
        {
            var topTours = await _showTours.GetTopToursAsync();
            return Ok(topTours);
        }

        [HttpGet("filters")] 
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

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddTour(CreateTourDto createTourDto)
        {
            await _showTours.AddTourAsync(createTourDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteTour(int id)
        {
            await _showTours.DeleteTourAsync(id);
            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("id")]
        public async Task<ActionResult> UpdateTour(int id, [FromQuery] UpdateTourDto updateTourDto)
        {
            await _showTours.UpdateTourAsync(id, updateTourDto);
            return NoContent();
        }
    }
}
