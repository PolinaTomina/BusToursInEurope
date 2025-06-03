using BusToursInEurope.Application.Models.AccountModel;
using Microsoft.AspNetCore.Http;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegistrationNewUserAsync(RegistrationDto registrationDto);
        Task<string> AuthUserAsync(AuthorizationDto authorizationDto);
        bool IsUserAuthenticated(HttpContext httpContext);
        Task ChangePasswordAsync(string email, string currentPassword, string newPassword);
    }
}
