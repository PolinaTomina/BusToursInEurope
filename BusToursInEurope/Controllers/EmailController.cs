using BusToursInEurope.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        [HttpGet("send")]
        public async Task SendMessage()
        {
            // Получатель письма.
            var recipientEmail = "polina6085@mail.ru";
            await _emailService.SendEmailAsync(recipientEmail, "Тема письма", "Тест письма: тест!");
        }
    }
}
