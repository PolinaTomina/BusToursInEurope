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
            return Ok();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteHotelBus(int id)
        {
            return Ok();
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
            return Ok();
        }
    }
}
