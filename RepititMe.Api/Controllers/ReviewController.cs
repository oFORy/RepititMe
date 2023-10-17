using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Reviews.Commands;
using RepititMe.Application.Services.Reviews.Queries;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object;

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

        [HttpGet("Api/Reviews/ShowAll")]
        public async Task<List<Review>> TeacherReview(int teacherId)
        {
            return await _reviewQueryService.TeacherReview(teacherId);
        }


        [HttpGet("Api/Reviews/NewReview")]
        public async Task<bool> NewReview([FromBody] ReviewObject reviewObject)
        {
            return await _reviewCommandService.NewReview(reviewObject);
        }
    }
}
