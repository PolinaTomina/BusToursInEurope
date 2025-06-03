using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    [Route("/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("block/{userId}")]
        public async Task<IActionResult> BlockUser(int userId)
        {
            var result = await _adminService.BlockUser(userId);
            if (!result) return NotFound("Пользователь не найден");

            return Ok("Пользователь заблокирован");
        }

        [HttpPost("unblock/{userId}")]
        public async Task<IActionResult> UnblockUser(int userId)
        {
            var result = await _adminService.UnblockUser(userId);
            if (!result) return NotFound("Пользователь не найден");

            return Ok("Пользователь разблокирован");
        }

        [HttpGet("reservations-users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_adminService.GetAllUsers());
        }
    }
}
