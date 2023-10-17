using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Students.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class StudentRepository : IStudentRepository
    {
        private readonly BotDbContext _botDbContext;
        public StudentRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public async Task<bool> ChangeProfile(int telegramId, string name)
        {
            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (user != null)
            {
                user.Name = name;
                _botDbContext.Users.Update(user);
                await _botDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }


        public async Task<List<Teacher>> ShowTeachers(List<int> lastTeachers)
        {
            var topTeachers = _botDbContext.Teachers
                    .Include(u => u.User)
                    .Include(u => u.Status)
                    .Include(u => u.Science)
                    .Include(u => u.LessonTarget)
                    .Include(u => u.AgeСategory)
                    .Where(t => t.Visibility != false && t.Block != true && !lastTeachers.Contains(t.UserId))
                    .OrderByDescending(e => e.PaymentRating)
                    .ThenByDescending(e => e.Rating)
                    .Take(5)
                    .ToList();


            return topTeachers;
        }

        public async Task<SignInStudentObject> SignInStudent(int telegramId)
        {


            var activityUpdate = _botDbContext.Users.FirstOrDefault(u => u.TelegramId == telegramId);

            if (activityUpdate != null)
            {
                activityUpdate.LastActivity = 1;
                _botDbContext.SaveChanges();
            }


            var topTeachers = _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeСategory)
                .Where(t => t.Visibility != false && t.Block != true)
                .OrderByDescending(e => e.PaymentRating)
                .ThenByDescending(e => e.Rating)
                .Take(5)
                .ToList();

            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            var usefulLinks = await _botDbContext.StudentUseFulUrls.ToListAsync();

            var signIn = new SignInStudentObject()
            {
                Name = user?.Name,
                Teachers = topTeachers,
                UsefulLinks = usefulLinks
            };

            return signIn;
        }

        public async Task<bool> SignOutStudent(int telegramId)
        {
            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (user != null)
            {
                user.LastActivity = 0;
                _botDbContext.Users.Update(user);
                await _botDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
