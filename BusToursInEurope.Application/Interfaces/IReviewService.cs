using BusToursInEurope.Application.Models.ReviewModels;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllByTourIdAsync(int tourId);
        Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReview, string email);
        Task DeleteReviewAsync(int id);
    }
}
