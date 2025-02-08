using BusToursInEurope.Application.Interfaces;
using BusToursInEurope.Application.Models.ReviewModels;
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
        [HttpPost(nameof(Create))]
        public async Task Create([FromBody] CreateReviewDto request)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.ValueType == ClaimTypes.Name)?.Value;
            await _reviewService.CreateReviewAsync(request, userEmail);
        }
    }
}