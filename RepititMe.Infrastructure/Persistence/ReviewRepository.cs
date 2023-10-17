using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object;
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
                IdTeacher = reviewObject.TeacherId,
                IdStudent = reviewObject.StudentId,
                DateTime = reviewObject.DateTime,
                Description = reviewObject.Description,
                Rating = reviewObject.Rating
            };
            _botDbContext.Reviews.Add(review);
            var saveResult = await _botDbContext.SaveChangesAsync();

            return saveResult > 0;
        }

        public async Task<List<Review>> TeacherReview(int teacherId)
        {
            var reviews = await _botDbContext.Reviews
                    .Where(r => r.IdTeacher == teacherId)
                    .ToListAsync();

            return reviews;
        }
    }
}
