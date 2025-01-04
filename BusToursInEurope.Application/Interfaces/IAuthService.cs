using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusToursInEurope.Core.Entites;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IAuthService
    {
        Task RegisterNewUserAsync(User user);
        Task<User> GetUserAsync(string login, string password);
    }
}
