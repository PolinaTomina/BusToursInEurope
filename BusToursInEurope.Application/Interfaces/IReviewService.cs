using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Models.ReviewModels;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllAsync();
        Task<List<ReviewDto>> GetAllByTourIdAsync(int tourId);
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReview, string email);
        Task<bool> DeleteReviewAsync(int reviewId, string email);
    }
}
