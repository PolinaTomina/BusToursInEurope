using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.UserModel;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationContext _context;

        public AdminService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> BlockUser(int userId)
        {
            {
                var user = await _context.Users.Include(u => u.Reservations) // Загружаем брони
                                               .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null) return false;

                // Удаляем бронирования пользователя
                _context.Reservations.RemoveRange(user.Reservations);

                // Устанавливаем флаг блокировки
                user.IsBlocked = true;

                await _context.SaveChangesAsync();
                return true;
            }
        }

        public Task<List<ShortUserDto>> GetAllUsers() =>
            _context.Users
                .Where(u => u.Role != "Admin")
                .Select(u => new ShortUserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Login = u.Login,
                    IsLocked = u.IsBlocked,
                })
                .ToListAsync();

        public async Task<bool> UnblockUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.IsBlocked = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
