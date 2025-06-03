using BusToursInEurope.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BusToursInEurope.Application
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ApplicationContext dbContext)
        {
            var emailClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name); // Ищем email в ClaimTypes.Name
            var email = emailClaim?.Value;

            if (!string.IsNullOrEmpty(email))
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null && user.IsBlocked)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Ваш аккаунт заблокирован.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
