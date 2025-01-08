using System.Net;
using BusToursInEurope.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.AccountModel;
using BusToursInEurope.Application.Services;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("reg")]
        public async Task<ActionResult> RegistrationNewUser([FromBody] RegistrationDto registrationDto)
        {
            var token = await _authService.RegistrationNewUserAsync(registrationDto);
            return Ok(new { Token = token });
        }

        [HttpPost("auth")]
        public async Task<ActionResult> AuthUserAsync(AuthorizationDto authorizationDto)
        {
            var token = await _authService.AuthUserAsync(authorizationDto);
            return Ok(new { Token = token });
        }
    }
}