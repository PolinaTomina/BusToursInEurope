using System.Security.Claims;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Services;

public class AuthService : IAuthService
{
    public Task RegisterNewUserAsync(User user)
    {
        //TODO: добавить бд
        return Task.CompletedTask;
    }

    public Task<User> GetUserAsync(string login, string password)
    {
        return Task.FromResult<User>(null);
    }
}