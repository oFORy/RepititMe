using RepititMe.Domain.Entities;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reviews.Common
{
    public interface IReviewRepository
    {
        Task<List<ReviewData>> TeacherReview(long telegramId);
        Task<bool> NewReview(ReviewObject reviewObject);
        Task<ReviewData> TeacherReviewData(int reviewId);
        Task<bool> ReviewSucces(ReviewSuccesObject reviewSuccesObject);
    }
}
