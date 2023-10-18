﻿using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Entities;
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

        public async Task<List<ReviewData>> TeacherReview(int teacherId)
        {
            return await _reviewRepository.TeacherReview(teacherId);
        }
    }
}
