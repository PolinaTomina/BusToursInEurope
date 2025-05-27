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
        [HttpPost("create review")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReview)
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("Не удалось определить пользователя");

            var result = await _reviewService.CreateReviewAsync(createReview, email);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("delete review")]
        public async Task<ActionResult> DeleteReview (int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return Ok();
        }
    }
}