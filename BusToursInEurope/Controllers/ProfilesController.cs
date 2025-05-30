using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ProfileModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [Authorize]
    [Route("profiles")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userEmail = User.Claims.First().Value;

            return Ok(await _profileService.GetProfileByUserEmailAsync(userEmail));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProfileDto request)
        {
            var userEmail = User.Claims.First().Value;

            await _profileService.UpdateProfileAsync(request, userEmail);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
