using RepititMe.Domain.Entities;
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
        Task<List<ReviewData>> TeacherReview(int teacherId);
        Task<bool> NewReview(ReviewObject reviewObject);
    }
}
