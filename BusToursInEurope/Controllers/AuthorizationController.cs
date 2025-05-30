using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.AccountModel;
using BusToursInEurope.Application.Contstants;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("reg")]
        public async Task<ActionResult> RegistrationNewUser([FromBody] RegistrationDto registrationDto)
        {
            var token = await _authService.RegistrationNewUserAsync(registrationDto);
            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult> AuthUserAsync(AuthorizationDto authorizationDto)
        {
            var token = await _authService.AuthUserAsync(authorizationDto);
            return Ok(new { Token = token });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("admin")]
        public IActionResult GetAdminData()
        {
            return Ok();
        }
    }
}