using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusToursInEurope.Application.Models.AccountModel;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegistrationNewUserAsync(RegistrationDto registrationDto);
        Task<string> AuthUserAsync(AuthorizationDto authorizationDto);
    }
}
