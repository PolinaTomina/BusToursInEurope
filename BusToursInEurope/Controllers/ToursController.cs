
using BusToursInEurope.Application;
using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.TourModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/tours")]
    public class ToursController : ControllerBase
    {
        private readonly ITours _showTours;
        private readonly IExportExcelService _exportExcelService;

        public ToursController(ITours showTours, IExportExcelService exportExcelService)
        {
            _showTours = showTours;
            _exportExcelService = exportExcelService;
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

        [Authorize(Roles = Role.Admin)]
        [HttpGet("export_statistic_top_tours")]
        public async Task<IActionResult> ExportTopTours()
        {
            var topTours = await _showTours.GetTopToursAsync();
            var fileContents = await _exportExcelService.ExportToursToExcel(topTours);

            return File(fileContents,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "TopTours.xlsx");
        }
    }
}
