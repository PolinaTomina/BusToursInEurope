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

    public AuthService(ApplicationContext context)
    {
        _context = context;
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
            NumPhone = registrationDto.NumPhone,
            Role = Role.User
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

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