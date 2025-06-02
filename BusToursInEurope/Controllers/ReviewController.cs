using BusToursInEurope.Application.Contstants;
using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReviewModels;
using BusToursInEurope.Core.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BusToursInEurope.Controllers
{
    [ApiController]
    [Route("/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Получение всех отзывов для тура
        /// </summary>
        /// <param name="tourId">Идентификатор тура</param>
        /// <returns>Список отзывов</returns>
        [HttpGet(nameof(GetAllByTourId))]
        public Task<List<ReviewDto>> GetAllByTourId([FromQuery] int tourId)
            => _reviewService.GetAllByTourIdAsync(tourId);

        /// <summary>
        /// Создание отзыва для тура
        /// </summary>
        /// <param name="request">Контракт для создания отзыва (оценка, коммент)</param>
        [Authorize]
        [HttpPost("create_review")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReview)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Не удалось определить пользователя");

            var result = await _reviewService.CreateReviewAsync(createReview, email);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("delete_review")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            var isAdmin = User.IsInRole(Role.Admin); // Используем твою константу

            if (string.IsNullOrEmpty(email))
                return Unauthorized();

            var success = await _reviewService.DeleteReviewAsync(reviewId, email, isAdmin);

            if (!success)
                return Forbid("Вы не можете удалить этот отзыв.");

            return NoContent();
        }
    }
}