using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ExelModel;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ExportExcelUserDto>> GetUsersAsync()
        {
            return await _context.Users
                .Select(u => new ExportExcelUserDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    NumPhone = u.NumPhone
                })
                .ToListAsync();
        }
    }
}
