using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.RoutesBusModels;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/routes")]
    public class RoutesBusController : ControllerBase
    {
        private readonly IRouteBusesService _routeService;

        public RoutesBusController(IRouteBusesService routeService)
        {
            _routeService = routeService;
        }

        [HttpPost(nameof(Create))]
        public Task Create([FromBody] CreateRouteBusDto request)
            => _routeService.AddRouteBusAsync(request);

        [HttpPost(nameof(Update))]
        public Task Update([FromBody] UpdateRouteBusDto request)
            => _routeService.UpdateRouteBusAsync(request);

        [HttpPost(nameof(Delete))]
        public Task Delete([FromBody] int id)
            => _routeService.DeleteRouteBusAsync(id);
    }
}