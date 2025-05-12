using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Application.Models.DbModel;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.ReviewModels;
using BusToursInEurope.Application.Models.RoutesBusModels;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.UserModel;
using BusToursInEurope.Application.Models.WayPointsModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class ToursService : ITours
    {
        private readonly ApplicationContext _context;

        public ToursService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<ShortTourDto>> GetTopToursAsync()
        {
            // получить список туров из БД: var entities = applicationCntext.Tours
            var tours = await _context.Tours.
                Select(t => new ShortTourDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Price = t.Price,
                    StartDate = t.StartDate,
                }).ToListAsync();

            return tours;
        }

        public async Task<List<ShortTourDto>> GetToursAsync(ToursFilter toursFilter)
        {
            var query = _context.Tours.AsQueryable();

            if (!string.IsNullOrEmpty(toursFilter.Country))
            {
                query = query.Where(t => t.Name.Contains(toursFilter.Country));
            }
            if (toursFilter.MinPrice.HasValue)
            {
                query = query.Where(t => t.Price >= toursFilter.MinPrice.Value);
            }
            if (toursFilter.MaxPrice.HasValue)
            {
                query = query.Where(t => t.Price <= toursFilter.MaxPrice.Value);
            }
            if (toursFilter.StartDate.HasValue) 
            { 
                query = query.Where(t => t.StartDate >= toursFilter.StartDate.Value); 
            }
            if (toursFilter.EndDate.HasValue)
            {
                query = query.Where(t => t.EndDate <= toursFilter.EndDate.Value);
            }

            var filteredTours = await query
                .Select(t => new ShortTourDto 
                { 
                    Id = t.Id, 
                    Name = t.Name, 
                    Price = t.Price, 
                    StartDate = t.StartDate, 
                }).ToListAsync(); 
            
            return filteredTours;

        }

        public async Task<FullTourDto> GetFullTourAsync(int id)
        {
            var tour = await _context.Tours
                .Include(t => t.Bus)
                .Include(t => t.RouteBus)
                    .ThenInclude(r => r.WayPoints)
                        .ThenInclude(wp => wp.City)
                .Include(t => t.RouteBus)
                    .ThenInclude(r => r.WayPoints)
                        .ThenInclude(wp => wp.Hotel)
                .Include(t => t.Reservations)
                    .ThenInclude(res => res.User)
                .Include(t => t.Reviews)
                    .ThenInclude(rev => rev.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tour == null)
                return null;

            return new FullTourDto
            {
                Name = tour.Name,
                Price = tour.Price,
                StartDate = tour.StartDate,
                EndDate = tour.EndDate,
                NumOfSeats = tour.NumOfSeats,
                Description = tour.Description,

                BusDto = tour.Bus != null ? new BusDto
                {
                    Id = tour.Bus.Id,
                    Name = tour.Bus.Name,
                    NumOfSeats = tour.Bus.NumOfSeats
                } : null,

                RouteBusDto = tour.RouteBus != null ? new RouteBusDto
                {
                    Id = tour.RouteBus.Id,
                    Distance = tour.RouteBus.Distance,
                    WayPointsDto = tour.RouteBus.WayPoints.Select(wp => new WayPointDto
                    {
                        Id = wp.Id,
                        NamePlace = wp.NamePlace,
                        CityDto = wp.City != null ? new CreateCityDto
                        {
                            Id = wp.City.Id,
                            Name = wp.City.Name,
                            Country = wp.City.Country,
                            Visa = wp.City.Visa
                        } : null,
                        HotelDto = wp.Hotel != null ? new HotelDto
                        {
                            Id = wp.Hotel.Id,
                            Name = wp.Hotel.Name,
                            Rating = wp.Hotel.Rating
                        } : null
                    }).ToList()
                } : null,

                ReservationsDto = tour.Reservations.Select(r => new ReservationDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    PaymentDeadline = r.PaymentDeadline,
                    PaymentDate = r.PaymentDate,
                    NumOfSeats = r.NumOfSeats,
                    UserDto = new UserDto
                    {
                        Id = r.User.Id,
                        Email = r.User.Email,
                        FullName = r.User.FullName
                    }
                }).ToList(),

                ReviewsDto = tour.Reviews.Select(rv => new ReviewDto
                {
                    Id = rv.Id,
                    Username = rv.User?.FullName,
                    Rating = rv.Rating,
                    Comment = rv.Comment,
                    ReviewDate = rv.ReviewDate
                }).ToList()
            };
        }

        public async Task AddTourAsync(CreateTourDto createTourDto)
        {
            // Проверка наличия автобуса
            var bus = await _context.Buses.FindAsync(createTourDto.BusId);
            if (bus == null)
            {
                throw new ArgumentException($"Автобус с ID {createTourDto.BusId} не найден");
            }

            // Проверка наличия маршрута
            var route = await _context.Routes.FindAsync(createTourDto.RouteBusId);
            if (route == null)
            {
                throw new ArgumentException($"Маршрут с ID {createTourDto.RouteBusId} не найден");
            }

            // Создание нового тура
            var tour = new Tour
            {
                Name = createTourDto.Name,
                Price = createTourDto.Price,
                StartDate = createTourDto.StartDate,
                EndDate = createTourDto.EndDate,
                NumOfSeats = createTourDto.NumOfSeats,
                Description = createTourDto.Description,
                Bus = bus, // Связь с автобусом
                RouteBus = route // Связь с маршрутом
            };

                await _context.Tours.AddAsync(tour);
                await _context.SaveChangesAsync();
        }

        public async Task DeleteTourAsync(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour != null)
            {
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTourAsync(int tourId, UpdateTourDto updateTourDto)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if(tour != null)
            {
                tour.Name = updateTourDto.Name;
                tour.Price = updateTourDto.Price;
                tour.StartDate = updateTourDto.StartDate;
                tour.EndDate = updateTourDto.EndDate;
                tour.NumOfSeats = updateTourDto.NumOfSeats;
                tour.Description = updateTourDto.Description;

                await _context.SaveChangesAsync();
            }
        }
    }
}
