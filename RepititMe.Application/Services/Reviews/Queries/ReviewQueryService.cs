using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reviews.Queries
{
    public class ReviewQueryService : IReviewQueryService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewQueryService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<Review>> TeacherReview(int teacherId)
        {
            return await _reviewRepository.TeacherReview(teacherId);
        }
    }
}
