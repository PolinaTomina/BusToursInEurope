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

        public async Task AddTourToProfile(string userEmail, int tourId)
        {
            var profileId = await _context.Profiles
                .AsNoTracking()
                .Include(p => p.User)
                .Where(p => p.User.Email == userEmail)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            var tour = await _context.Tours.FindAsync(tourId);

            if (profileId != default && tour != null)
            {
                await _context.ProfilesTours.AddAsync(new ProfileTour
                {
                    ProfileId = profileId,
                    TourId = tourId
                });

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShortTourDto>> GetProfileTours(string userEmail)
        {
            var profileId = await _context.Profiles
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.User.Email == userEmail)
                .Select (x => x.Id)
                .FirstOrDefaultAsync();

            var toursId = await _context.ProfilesTours
                .Where(x => x.ProfileId == profileId)
                .Select(x => x.TourId)
                .ToListAsync();

            return await _context.Tours
                .Where(x => toursId.Any(id => id == x.Id))
                .Select(t => new ShortTourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    FirstImageLink = t.ImageLinks.First(),
                    IsLiked = true
                })
                .ToListAsync();
        }
    }
}
