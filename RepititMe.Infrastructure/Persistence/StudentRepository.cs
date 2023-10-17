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

        public async Task<List<BriefTeacher>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject)
        {
            var topTeachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeСategory)
                .Where(t => t.Visibility != false
                            && t.Block != true
                            && t.ScienceId == searchCategoriesResultObject.ScienceId
                            && t.LessonTargetId == searchCategoriesResultObject.LessonTargetId
                            && t.AgeСategoryId == searchCategoriesResultObject.AgeСategoryId
                            && t.StatusId == searchCategoriesResultObject.StatusId
                            && t.Price >= searchCategoriesResultObject.LowPrice
                            && t.Price <= searchCategoriesResultObject.HighPrice)
                .OrderByDescending(e => e.Rating)
                .ThenByDescending(e => e.PaymentRating)
                .Take(5)
                .Select(t => new BriefTeacher
                {
                    User = t.User,
                    Image = t.Image,
                    Status = t.Status,
                    Science = t.Science,
                    LessonTarget = t.LessonTarget,
                    AgeСategory = t.AgeСategory,
                    Experience = t.Experience,
                    Description = t.Description,
                    Price = t.Price,
                    Rating = t.Rating
                })
                .ToListAsync();

            return topTeachers;
        }




        public async Task<SearchCategoriesObject> SearchCategories()
        {
            var result = new SearchCategoriesObject()
            {
                AgeCategories = await _botDbContext.AgeСategories.ToListAsync(),
                LessonTargets = await _botDbContext.LessonTargets.ToListAsync(),
                Sciences = await _botDbContext.Sciences.ToListAsync(),
                TeacherStatuses = await _botDbContext.TeacherStatuses.ToListAsync()
            };

            return result;
        }

        public async Task<List<BriefTeacher>> ShowTeachers(List<int> lastTeachers)
        {
            var topTeachers = await _botDbContext.Teachers
                    .Include(u => u.User)
                    .Include(u => u.Status)
                    .Include(u => u.Science)
                    .Include(u => u.LessonTarget)
                    .Include(u => u.AgeСategory)
                    .Where(t => t.Visibility != false && t.Block != true && !lastTeachers.Contains(t.UserId))
                    .OrderByDescending(e => e.PaymentRating)
                    .ThenByDescending(e => e.Rating)
                    .Take(5)
                    .Select(t => new BriefTeacher
                    {
                        User = t.User,
                        Image = t.Image,
                        Status = t.Status,
                        Science = t.Science,
                        LessonTarget = t.LessonTarget,
                        AgeСategory = t.AgeСategory,
                        Experience = t.Experience,
                        Description = t.Description,
                        Price = t.Price,
                        Rating = t.Rating
                    })
                    .ToListAsync();

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


            var topTeachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeСategory)
                .Where(t => t.Visibility != false && t.Block != true)
                .OrderByDescending(e => e.PaymentRating)
                .ThenByDescending(e => e.Rating)
                .Take(5)
                .Select(t => new BriefTeacher
                {
                    User = t.User,
                    Image = t.Image,
                    Status = t.Status,
                    Science = t.Science,
                    LessonTarget = t.LessonTarget,
                    AgeСategory = t.AgeСategory,
                    Experience = t.Experience,
                    Description = t.Description,
                    Price = t.Price,
                    Rating = t.Rating
                })
                    .ToListAsync();

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
