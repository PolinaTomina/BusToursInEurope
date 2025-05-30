using BusToursInEurope.Application.Models.ProfileModels;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IProfileService
    {
        Task CreateEmptyProfileAsync(int userId);

        Task UpdateProfileAsync(UpdateProfileDto request, string userEmail);

        Task<GetProfileDto> GetProfileByUserEmailAsync(string userEmail);
    }
}
