using BusToursInEurope.Application.Models.ReviewModels;

namespace BusToursInEurope.Application.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetAllByTourIdAsync(int tourId);
        Task CreateReviewAsync(CreateReviewDto request, string email);
    }
}
