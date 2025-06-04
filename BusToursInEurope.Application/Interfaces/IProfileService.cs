using BusToursInEurope.Application.Models.ProfileModels;
using BusToursInEurope.Application.Models.TourModel;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IProfileService
    {
        Task CreateEmptyProfileAsync(int userId);

        Task UpdateProfileAsync(UpdateProfileDto request, string userEmail);

        Task<GetProfileDto> GetProfileByUserEmailAsync(string userEmail);

        Task AddTourToProfile(string userEmail, int tourId);

        Task<List<ShortTourDto>> GetProfileTours(string userEmail);
    }
}
