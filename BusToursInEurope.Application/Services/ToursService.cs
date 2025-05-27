using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.CityModel;
using BusToursInEurope.Application.Models.BusModel;
using BusToursInEurope.Application.Models.HotelModel;
using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.ReviewModels;
using BusToursInEurope.Application.Models.RoutesBusModels;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.UserModel;
using BusToursInEurope.Application.Models.WayPointsModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
                    EndDate = t.EndDate,
                    FirstImageLink = t.ImageLinks.FirstOrDefault()
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
                FullImageLink = tour.ImageLinks,

                BusDto = tour.Bus != null ? new ShowBusDto
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
                        CityDto = wp.City != null ? new ShowCityDto
                        {
                            Id = wp.City.Id,
                            Name = wp.City.Name,
                            Country = wp.City.Country,
                            Visa = wp.City.Visa
                        } : null,
                        HotelDto = wp.Hotel != null ? new ShowHotelDto
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
                    Login = rv.Login,
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
            var route = await _context.RoutesBuses.FindAsync(createTourDto.RouteBusId);
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

            string tourPath = Path.Combine("TourFiles", tour.Name.ToString());
            Directory.CreateDirectory(tourPath);

            foreach (var image in createTourDto.Images)
            {
                string imagePath = Path.Combine(tourPath, image.FileName);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                tour.ImageLinks.Add(imagePath);

            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTourAsync(int tourId)
        {
            var tour = await _context.Tours.FindAsync(tourId);

            if (tour == null)
            {
                return;
            }

            string tourPath = Path.Combine("TourFiles", tour.Name.ToString());

            if (tour.ImageLinks != null)
            {
                foreach (var imagePath in tour.ImageLinks)
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
            }

            if (Directory.Exists(tourPath))
            {
                Directory.Delete(tourPath, true);
            }

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTourAsync(int tourId, UpdateTourDto updateTourDto)
        {
            var tour = await _context.Tours.FindAsync(tourId);
            if (tour != null)
            {
                if (!string.IsNullOrEmpty(updateTourDto.Name)) tour.Name = updateTourDto.Name;
                if (updateTourDto.Price.HasValue) tour.Price = updateTourDto.Price.Value;
                if (updateTourDto.StartDate.HasValue) tour.StartDate = updateTourDto.StartDate.Value;
                if (updateTourDto.EndDate.HasValue) tour.EndDate = updateTourDto.EndDate.Value;
                if (updateTourDto.NumOfSeats.HasValue) tour.NumOfSeats = updateTourDto.NumOfSeats.Value;
                if (!string.IsNullOrEmpty(updateTourDto.Description)) tour.Description = updateTourDto.Description;

                string tourPath = Path.Combine("TourFiles", tour.Name.ToString());

                if (updateTourDto.Images != null && updateTourDto.Images.Any())
                {
                    if (Directory.Exists(tourPath))
                    {
                        Directory.Delete(tourPath, true);
                    }

                    Directory.CreateDirectory(tourPath);

                    tour.ImageLinks.Clear();

                    foreach (var image in updateTourDto.Images)
                    {
                        string imagePath = Path.Combine(tourPath, image.FileName);

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        tour.ImageLinks.Add(imagePath);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
