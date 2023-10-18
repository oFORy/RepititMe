using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reviews.Commands
{
    public class ReviewCommandService : IReviewCommandService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewCommandService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> NewReview(ReviewObject reviewObject)
        {
            return await _reviewRepository.NewReview(reviewObject);
        }
    }
}
