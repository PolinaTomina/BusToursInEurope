using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/tours1")]
    public class ShowToursController : ControllerBase
    {
        private static readonly string[] Tours = new[]
        {
            "One", "Two", "Three", "Four", "Five"
        };

        private readonly ILogger<ShowToursController> _logger;

        public ShowToursController(ILogger<ShowToursController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IEnumerable<string> Get(int count)
        {
            return Tours.Take(count);
        }
    }
}
