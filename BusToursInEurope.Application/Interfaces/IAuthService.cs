using BusToursInEurope.Application.Models.AccountModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegistrationNewUserAsync(RegistrationDto registrationDto);
        Task<string> AuthUserAsync(AuthorizationDto authorizationDto);
    }
}
