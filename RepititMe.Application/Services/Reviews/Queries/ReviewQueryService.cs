﻿using RepititMe.Application.Services.Orders.Common;
using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reviews;
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

        public async Task<List<ReviewData>> TeacherReview(long telegramId)
        {
            return await _reviewRepository.TeacherReview(telegramId);
        }

        public async Task<ReviewData> TeacherReviewData(int reviewId)
        {
            return await _reviewRepository.TeacherReviewData(reviewId);
        }

        public async Task<bool> ReviewSucces(ReviewSuccesObject reviewSuccesObject)
        {
            return await _reviewRepository.ReviewSucces(reviewSuccesObject);
        }
    }
}
