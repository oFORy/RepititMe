﻿using RepititMe.Domain.Entities;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reviews.Queries
{
    public interface IReviewQueryService
    {
        Task<List<ReviewData>> TeacherReview(long telegramId);
        Task<ReviewData> TeacherReviewData(int reviewId);
        Task<bool> ReviewSucces(ReviewSuccesObject reviewSuccesObject);
    }
}
