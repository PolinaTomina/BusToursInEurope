using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReviewModels;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationContext _context;

        public ReviewService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto createReview, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                throw new Exception("Пользователь не найден");

            var review = new Review
            {
                Rating = createReview.Rating,
                Comment = createReview.Comment,
                ReviewDate = DateTime.UtcNow,
                Login = user.Login,
                UserId = user.Id,
                TourId = createReview.TourId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return new ReviewDto
            {
                Id = review.Id,
                Login = user.Login,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                UserId = user.Id,
                TourId = review.TourId
            };
        }

        public async Task<List<ReviewDto>> GetAllByTourIdAsync(int tourId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.TourId == tourId)
                .ToListAsync();

            return reviews.Select(r => new ReviewDto
            {
                Id = r.Id,
                Login = r.Login,
                Rating = r.Rating,
                Comment = r.Comment,
                ReviewDate = r.ReviewDate,
                UserId = r.UserId,
                TourId = r.TourId,
            }).ToList();
        }

        public async Task DeleteReviewAsync (int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new Exception("Отзыв не найден");
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

}
