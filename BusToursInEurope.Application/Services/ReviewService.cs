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

        public async Task CreateReviewAsync(CreateReviewDto request, string email)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.User.Email == email);

            var user = await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Email == email);

            if (review != null || user == null)
            {
                throw new ApplicationException("Невозможно оставить отзыв");
            }

            review = new Review
            {
                UserId = user.Id,
                Rating = request.Rating,
                Comment = request.Comment,
                ReviewDate = DateTime.Now,
                FullName = user.FullName,
                TourId = request.TourId,
            };

            await _context.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public Task<List<ReviewDto>> GetAllByTourIdAsync(int tourId) 
            => _context.Reviews
                .AsNoTracking()
                .Where(r => r.TourId == tourId)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    ReviewDate = r.ReviewDate,
                    Rating = r.Rating,
                    TourId = r.TourId,
                    Username = r.User.UserName != null 
                        ? r.User.UserName 
                        : ReviewConstants.IncogUsername,
                    UserId = r.User.Id
                })
                .ToListAsync();
    }
}
