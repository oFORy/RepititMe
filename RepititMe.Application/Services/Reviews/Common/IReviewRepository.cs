using RepititMe.Domain.Entities;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reviews.Common
{
    public interface IReviewRepository
    {
        Task<List<Review>> TeacherReview(int teacherId);
        Task<bool> NewReview(ReviewObject reviewObject);
    }
}
