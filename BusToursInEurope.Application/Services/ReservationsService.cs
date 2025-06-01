using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReservationModel;
using BusToursInEurope.Application.Models.TourModel;
using BusToursInEurope.Application.Models.UserModel;
using BusToursInEurope.Core.Entites;
using BusToursInEurope.Database;
using Microsoft.EntityFrameworkCore;

namespace BusToursInEurope.Application.Services
{
    public class ReservationsService : IReservations
    {
        private readonly ApplicationContext _context;

        public ReservationsService(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавить бронирование
        /// </summary>
        public async Task AddReservationAsync(CreateReservationDto reservationDto, string userEmail)
        {
            var user = await _context.Users
                .SingleAsync(x => x.Email == userEmail);

            if (user is null)
            {
                throw new KeyNotFoundException("Пользователь не найден");
            }

            var tour = await _context.Tours
                .FindAsync(reservationDto.TourId);

            if (tour == null)
            {
                throw new KeyNotFoundException("Тур не найден");
            }

            // Проверка доступности мест
            if (tour.NumOfSeats < reservationDto.NumOfSeats)
            {
                throw new InvalidOperationException("Недостаточно свободных мест");
            }

            var reservation = new Reservation
            {
                Date = DateTime.UtcNow,
                PaymentDeadline = tour.StartDate.AddDays(-3),
                NumOfSeats = reservationDto.NumOfSeats,
                UserId = user.Id,
                TourId = tour.Id
            };

            await _context.Reservations.AddAsync(reservation);

            // Обновляем количество доступных мест
            tour.NumOfSeats -= reservationDto.NumOfSeats;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить бронирование
        /// </summary>
        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Бронирование не найдено");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получить все бронирования (это для пользователя)
        /// </summary>
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .AsNoTracking()
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    PaymentDate = r.PaymentDate,
                    PaymentDeadline = r.PaymentDeadline,
                    NumOfSeats = r.NumOfSeats,
                    UserId = r.UserId
                })
                .ToListAsync();
        }

        /// <summary>
        /// Получить бронирование по Id (это для админа)
        /// </summary>
        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .AsNoTracking()
                .SingleAsync(r => r.Id == id);

            if (reservation == null)
            {
                throw new KeyNotFoundException("Бронирование не найдено");
            }

            return new ReservationDto
            {
                Id = reservation.Id,
                Date = reservation.Date,
                PaymentDate = reservation.PaymentDate,
                PaymentDeadline = reservation.PaymentDeadline,
                NumOfSeats = reservation.NumOfSeats,
                UserId = reservation.UserId
            };
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(string userEmail)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(r => r.User.Email == userEmail)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    PaymentDate = r.PaymentDate,
                    PaymentDeadline = r.PaymentDeadline,
                    NumOfSeats = r.NumOfSeats,
                    UserId = r.UserId
                })
                .ToListAsync();
        }

        public async Task UpdatePaymentStatusAsync(UpdatePaymentStatusDto request)
        {
            var reservation = await _context.Reservations.FindAsync(request.Id);

            if (reservation is null)
            {
                throw new KeyNotFoundException("Бронь не найдена");
            }

            reservation.PaymentDate = request.IsPaid ? DateTime.Now : null;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ReservationExportDto>> GetReservationsForExportAsync()
        {
            var reservations = await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Tour)
                .Select(r => new ReservationExportDto
                {
                    ReservationId = r.Id,
                    Date = r.Date,
                    PaymentDate = r.PaymentDate,
                    PaymentDeadline = r.PaymentDeadline,
                    NumOfSeats = r.NumOfSeats,
                    UserEmail = new UserDto { Email = r.User.Email },
                    ExportExcelReservationTour = new ExportExcelReservationTourDto { TourId = r.Tour.Id, TourName = r.Tour.Name }
                })
                .ToListAsync();

            return reservations;
        }
    }
}
