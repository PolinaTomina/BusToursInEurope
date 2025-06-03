using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ProfileModels;
using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.ReviewModels;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.UserModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationContext _context;

        public ProfileService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateEmptyProfileAsync(int userId)
        {
            var newProfile = new Profile
            {
                UserId = userId,
            };

            await _context.AddAsync(newProfile);
            await _context.SaveChangesAsync();
        }

        public async Task<GetProfileDto> GetProfileByUserEmailAsync(string userEmail)
        {
            var userProfile = await _context.Profiles
                .AsNoTracking()
                .Include(p => p.User)
                .ThenInclude(u => u.Reservations)
                .Include(p => p.User)
                .ThenInclude(u => u.Reviews)
                .Where(p => p.User.Email == userEmail)
                .SingleOrDefaultAsync();

            if (userProfile is null)
            {
                throw new ApplicationException("Пользователь с данным email отсуствует");
            }

            return new GetProfileDto
            {
                Id = userProfile.Id,
                Name = userProfile.Name,
                SurName = userProfile.SurName,
                MiddleName = userProfile.MiddleName,
                NumPhone = userProfile.NumPhone,
                PassportNumber = userProfile.PassportNumber,
                User = new UserDto
                {
                    Id = userProfile.UserId,
                    Email = userProfile.User.Email,
                    Login = userProfile.User.Login,
                    Password = userProfile.User.Password,
                    Role = userProfile.User.Role,
                    ReservationsDto = userProfile.User.Reservations.Select(x => new ReservationDto
                    {
                        Id = x.Id,
                        Date = x.Date,
                        PaymentDate = x.PaymentDate,
                        PaymentDeadline = x.PaymentDeadline,
                        UserId = x.UserId,
                        NumOfSeats = x.NumOfSeats,
                    }).ToList(),
                    ReviewsDto = userProfile.User.Reviews.Select(x => new ReviewDto
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        ReviewDate = x.ReviewDate,
                        Rating = x.Rating,
                        TourId = x.TourId,
                    }).ToList()
                }
            };
        }

        public async Task UpdateProfileAsync(UpdateProfileDto request, string userEmail)
        {
            var user = await _context.Users.SingleAsync(u => u.Email == userEmail);

            if (user is null)
            {
                throw new ApplicationException("Пользователь с данным email отсуствует");
            }

            var profile = await _context.Profiles.SingleOrDefaultAsync(p => p.UserId == user.Id);

            if (profile is null)
            {
                profile = new Profile
                {
                    UserId = user.Id,
                };

                await _context.AddAsync(profile);
            }

            profile.Name = request.Name;
            profile.SurName = request.SurName;
            profile.MiddleName = request.MiddleName;
            profile.NumPhone = request.NumPhone;
            profile.PassportNumber = request.PassportNumber;

            await _context.SaveChangesAsync();
        }

        public async Task AddTourToProfile(int profileId, int tourId)
        {
            var profile = await _context.Profiles.Include(p => p.Tours)
                .FirstOrDefaultAsync(p => p.Id == profileId);

            var tour = await _context.Tours.FindAsync(tourId);

            if (profile != null && tour != null)
            {
                profile.Tours.Add(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShortTourDto>> GetProfileTours(int profileId)
        {
            return await _context.Profiles
                .Where(p => p.Id == profileId)
                .SelectMany(p => p.Tours)
                .Select(t => new ShortTourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    FirstImageLink = t.ImageLinks.First()
                })
                .ToListAsync();
        }
    }
}
