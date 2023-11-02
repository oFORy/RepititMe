using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Reviews.Commands;
using RepititMe.Application.Services.Reviews.Queries;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reviews;
using System.ComponentModel;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class ReviewController : Controller
    {
        private readonly IReviewCommandService _reviewCommandService;
        private readonly IReviewQueryService _reviewQueryService;

        public ReviewController(IReviewCommandService reviewCommandService, IReviewQueryService reviewQueryService)
        {
            _reviewCommandService = reviewCommandService;
            _reviewQueryService = reviewQueryService;
        }

        /// <summary>
        /// Показать все отзывы учителя
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        [HttpGet("Api/Reviews/ShowAll")]
        public async Task<List<ReviewData>> TeacherReview(long telegramId)
        {
            return await _reviewQueryService.TeacherReview(telegramId);
        }

        /// <summary>
        /// Оставить отзыв
        /// </summary>
        /// <param name="reviewObject"></param>
        /// <returns></returns>
        [HttpPost("Api/Reviews/NewReview")]
        public async Task<bool> NewReview([FromBody] ReviewObject reviewObject)
        {
            return await _reviewCommandService.NewReview(reviewObject);
        }


        [HttpGet("Api/Reviews/Data")]
        public async Task<ReviewData> TeacherReviewData(int reviewId)
        {
            return await _reviewQueryService.TeacherReviewData(reviewId);
        }

        [HttpPost("Api/Reviews/ReviewSucces")]
        public async Task<bool> ReviewSucces([FromBody] ReviewSuccesObject reviewSuccesObject)
        {
            return await _reviewQueryService.ReviewSucces(reviewSuccesObject);
        }
    }
}
