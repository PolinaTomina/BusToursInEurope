using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.AccountModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BusToursInEurope.Application.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationContext _context;
    private readonly IProfileService _profileService;
    private readonly IEmailService _emailService;

    public AuthService(ApplicationContext context, IProfileService profileService, IEmailService emailService)
    {
        _context = context;
        _profileService = profileService;
        _emailService = emailService;
    }

    public async Task<string> RegistrationNewUserAsync(RegistrationDto registrationDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registrationDto.Email))
        {
            throw new Exception("Пользователь с такой электронной почтой уже существует.");
        }

        var user = new User
        {
            Login = registrationDto.Login,
            Email = registrationDto.Email,
            Password = registrationDto.Password,
            Role = Role.User
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        await _profileService.CreateEmptyProfileAsync(user.Id);

        return CreateJwtToken(user.Email, user.Role);
    }

    public async Task<string> AuthUserAsync(AuthorizationDto authorizationDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == authorizationDto.Email);
        if (user == null)
        {
            throw new Exception("Пользователь не найден");
        }

        if (user.Password != authorizationDto.Password)
        {
            throw new Exception("Неверный пароль");
        }

        // Создание JWT токена
        return CreateJwtToken(user.Email, user.Role);
    }

    public async Task ChangePasswordAsync(string email, string currentPassword, string newPassword)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("Пользователь не найден.");
        }

        if (user.Password != currentPassword)
        {
            throw new Exception("Неверный текущий пароль.");
        }

        user.Password = newPassword;
        await _context.SaveChangesAsync();

        string subject = "🔐 Пароль успешно изменен";
        string message = $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    .container {{ max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px; text-align: center; padding: 20px; }}
                    h2 {{ color: #2E86C1; }}
                    p {{ font-size: 16px; }}
                    .footer {{ font-size: 12px; color: #555; margin-top: 20px; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>🔐 Ваш пароль успешно изменен</h2>
                    <p>Здравствуйте! Ваш пароль был изменен. Если это были не вы, пожалуйста, немедленно свяжитесь с поддержкой.</p>
                    <p class='footer'>Это автоматическое уведомление. Спасибо за использование нашего сервиса! 🚀</p>
                </div>
            </body>
            </html>";

        await _emailService.SendEmailAsync(email, subject, message);
    }

    public static string CreateJwtToken(string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(ClaimTypes.Role, role)
        };

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}