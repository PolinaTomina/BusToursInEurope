using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReservationModel;
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
        public async Task AddReservationAsync(CreateReservationDto reservationDto)
        {
            
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
        /// Обновить бронирование
        /// </summary>
        public async Task UpdateReservationAsync(int id, CreateReservationDto reservationDto)
        {
           
        }

        /// <summary>
        /// Получить все бронирования
        /// </summary>
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    Date = r.Date,
                    PaymentDate = r.PaymentDate,
                    PaymentDeadline = r.PaymentDeadline,
                    NumOfSeats = r.NumOfSeats,
                    UserDtoId = r.UserId
                })
                .ToListAsync();
        }

        /// <summary>
        /// Получить бронирование по Id
        /// </summary>
        public async Task<ReservationDto> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
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
                UserDtoId = reservation.UserId
            };
        }
    }
}
