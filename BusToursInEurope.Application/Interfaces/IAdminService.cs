using BusToursInEurope.Application.Models.UserModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IAdminService
    {
        Task<bool> BlockUser(int userId);
        Task<bool> UnblockUser(int userId);
        Task<List<ShortUserDto>> GetAllUsers();
    }
}
