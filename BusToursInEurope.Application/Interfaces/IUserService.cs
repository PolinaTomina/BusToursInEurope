using BusToursInEurope.Application.Models.ExelModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<ExportExcelUserDto>> GetUsersAsync();
    }
}
