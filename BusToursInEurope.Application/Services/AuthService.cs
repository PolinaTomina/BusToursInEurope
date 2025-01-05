using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Core.Entites;
using Microsoft.IdentityModel.Tokens;

namespace BusToursInEurope.Application.Services;

public class AuthService : IAuthService
{
    public Task RegisterNewUserAsync(User user)
    {
        //TODO: добавить бд
        return Task.CompletedTask;
    }

    //TODO: переимновать метод
    public Task<User> GetUserAsync(string login, string password)
    {
        return Task.FromResult<User>(null);
    }

    public static string CreateJwtToken(string login)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login)
        };

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}