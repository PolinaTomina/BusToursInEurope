using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/hotels")]

    public class HotelsController : ControllerBase
    {
        private readonly IHotels _hotels;

        public HotelsController(IHotels hotels)
        {
            _hotels = hotels;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddHotel(HotelDto hotelDto)
        {
            await _hotels.AddHotelAsync(hotelDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("id")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            await _hotels.DeleteHotelAsync(id);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut("id")]
        public async Task<ActionResult> UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
            await _hotels.UpdateHotelAsync(id, hotelDto);
            return StatusCode(201);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("filters")]
        public async Task<IActionResult> GetHotels([FromQuery] HotelFilter hotelFilter)
        {
            var hotels = await _hotels.GetHotelsAsync(hotelFilter);
            return Ok(hotels);
        }

    }
}
