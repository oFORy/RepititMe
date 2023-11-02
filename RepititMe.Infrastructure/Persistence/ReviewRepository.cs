using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Object.Orders;
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
            var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == reviewObject.TelegramIdTeacher)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var teacherId = await _botDbContext.Teachers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();

            var userId2 = await _botDbContext.Users
                .Where(u => u.TelegramId == reviewObject.TelegramIdStudent)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var studentId = await _botDbContext.Students
                .Where(s => s.UserId == userId2)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();

            var review = new Review
            {
                TeacherId = teacherId,
                StudentId = studentId,
                DateTime = DateTime.UtcNow,
                Description = reviewObject.Description,
                Rating = reviewObject.Rating
            };
            _botDbContext.Reviews.Add(review);

            // Сюда отправка уведомления о новом отзыве


            //
            return await _botDbContext.SaveChangesAsync() > 0;
        }



        public async Task<bool> ReviewSucces(ReviewSuccesObject reviewSuccesObject)
        {
            var orderId = await _botDbContext.Orders
                .Include(s => s.Student)
                    .ThenInclude(s => s.User)
                .Include(t => t.Teacher)
                    .ThenInclude(t => t.User)
                .Where(r => r.Teacher.User.TelegramId == reviewSuccesObject.TelegramIdTeacher && r.Student.User.TelegramId == reviewSuccesObject.TelegramIdStudent)
                .Select(i => i.Id)
                .FirstOrDefaultAsync();

            var check = await _botDbContext.Reports.Where(o => o.OrderId == orderId).ToListAsync();

            if (check.Count >= 2)
            {
                return true;
            }
            else
                return false;
        }

        public async Task<List<ReviewData>> TeacherReview(long telegramId)
        {
            var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var teacherId = await _botDbContext.Teachers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();


            var reviews = await _botDbContext.Reviews
                .Where(t => t.TeacherId == teacherId)
                .Include(s => s.Student)
                    .ThenInclude(u => u.User)
                .Select(r => new ReviewData
                {
                    Id = r.Id,
                    Student = r.Student,
                    DateTime = r.DateTime,
                    Description = r.Description,
                    Rating = r.Rating
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<ReviewData> TeacherReviewData(int reviewId)
        {
            var review = await _botDbContext.Reviews
                .Where(i => i.Id == reviewId)
                .Include(s => s.Student)
                    .ThenInclude(u => u.User)
                .Select(r => new ReviewData
                {
                    Id = r.Id,
                    Student = r.Student,
                    DateTime = r.DateTime,
                    Description = r.Description,
                    Rating = r.Rating
                })
                .SingleAsync();

            return review;
        }
    }
}
