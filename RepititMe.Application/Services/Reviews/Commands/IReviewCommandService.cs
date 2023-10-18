using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reviews.Commands
{
    public interface IReviewCommandService
    {
        Task<bool> NewReview(ReviewObject reviewObject);
    }
}
