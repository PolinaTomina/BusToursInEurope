using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.HotelModel;
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

        [HttpPost]
        public async Task<ActionResult> AddHotel(HotelDto hotelDto)
        {
            await _hotels.AddHotelAsync(hotelDto);
            return StatusCode(201);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            await _hotels.DeleteHotelAsync(id);
            return StatusCode(201);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
            await _hotels.UpdateHotelAsync(id, hotelDto);
            return StatusCode(201);
        }
    }
}
