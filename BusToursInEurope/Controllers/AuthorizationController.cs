using System.Net;
using BusToursInEurope.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusToursInEurope.Application.Interfaces;
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

        [HttpPost("login")]
        public async Task<string> LoginAsync(string login, string password)
        {
            var user = await _authService.GetUserAsync(login, password);

            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return null;
            }

            return AuthService.CreateJwtToken(login);
        }

        [HttpPost("register")]
        public async Task<string> Register(string login, string password)
        {
            var newUser = new User()
            {
                Email = "user@user.com",
                Fio = "UserLastName",
                NumPhone = "+375 (29) 123-45-67",
                UserName = login,
                Password = password
            };

            await _authService.RegisterNewUserAsync(newUser);

            return AuthService.CreateJwtToken(login);
        }
    }
}