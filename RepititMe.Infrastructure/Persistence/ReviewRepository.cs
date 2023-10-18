using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BotDbContext _botDbContext;
        public ReviewRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public async Task<bool> NewReview(ReviewObject reviewObject)
        {
            var review = new Review
            {
                TeacherId = reviewObject.TeacherId,
                StudentId = reviewObject.StudentId,
                DateTime = reviewObject.DateTime,
                Description = reviewObject.Description,
                Rating = reviewObject.Rating
            };
            _botDbContext.Reviews.Add(review);
            var saveResult = await _botDbContext.SaveChangesAsync();

            return saveResult > 0;
        }

        public async Task<List<ReviewData>> TeacherReview(int teacherId)
        {
            var reviews = await _botDbContext.Reviews
                .Where(r => r.TeacherId == teacherId)
                .Include(r => r.Student)
                    .ThenInclude(s => s.User)
                .Select(r => new ReviewData
                {
                    Student = r.Student,
                    DateTime = r.DateTime,
                    Description = r.Description,
                    Rating = r.Rating
                })
                .ToListAsync();

            return reviews;
        }
    }
}
