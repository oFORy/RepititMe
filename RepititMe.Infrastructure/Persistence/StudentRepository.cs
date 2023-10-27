using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Students.Common;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Sciences;
using RepititMe.Domain.Object.SearchCategory;
using RepititMe.Domain.Object.Students;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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


        public async Task<List<BriefTeacherObject>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject)
        {
            var topTeachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeCategory)
                .Where(t => t.Visibility != false
                            && t.Block != true
                            && t.ScienceId == searchCategoriesResultObject.ScienceId
                            && t.LessonTargetId == searchCategoriesResultObject.LessonTargetId
                            && t.AgeCategoryId == searchCategoriesResultObject.AgeCategoryId
                            && t.StatusId == searchCategoriesResultObject.StatusId
                            && t.Price >= searchCategoriesResultObject.LowPrice
                            && t.Price <= searchCategoriesResultObject.HighPrice)
                .OrderByDescending(e => e.Rating)
                .ThenByDescending(e => e.PaymentRating)
                .Take(5)
                .Select(t => new BriefTeacherObject
                {
                    User = t.User,
                    Image = t.Image,
                    Status = t.Status,
                    Science = t.Science,
                    LessonTarget = t.LessonTarget,
                    AgeCategory = t.AgeCategory,
                    Experience = t.Experience,
                    AboutMe = t.AboutMe,
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
                AgeCategories = await _botDbContext.AgeCategories.ToListAsync(),
                Sciences = await _botDbContext.Sciences
                    .Include(s => s.ScienceLessonTargets)
                        .ThenInclude(slt => slt.LessonTarget)
                    .Select(s => new ScienceDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        LessonTargets = s.ScienceLessonTargets
                            .Select(slt => new LessonTargetDto
                            {
                                Id = slt.LessonTargetId,
                                Name = slt.LessonTarget.Name
                            })
                            .ToList()
                    })
                    .ToListAsync(),
                TeacherStatuses = await _botDbContext.TeacherStatuses.ToListAsync()
            };

            return result;
        }

        public async Task<List<BriefTeacherObject>> ShowTeachers(List<int> lastTeachers)
        {
            var topTeachers = await _botDbContext.Teachers
                    .Include(u => u.User)
                    .Include(u => u.Status)
                    .Include(u => u.Science)
                    .Include(u => u.LessonTarget)
                    .Include(u => u.AgeCategory)
                    .Where(t => t.Visibility != false && t.Block != true && !lastTeachers.Contains(t.UserId))
                    .OrderByDescending(e => e.PaymentRating)
                    .ThenByDescending(e => e.Rating)
                    .Take(5)
                    .Select(t => new BriefTeacherObject
                    {
                        User = t.User,
                        Image = t.Image,
                        Status = t.Status,
                        Science = t.Science,
                        LessonTarget = t.LessonTarget,
                        AgeCategory = t.AgeCategory,
                        Experience = t.Experience,
                        AboutMe = t.AboutMe,
                        Price = t.Price,
                        Rating = t.Rating
                    })
                    .ToListAsync();

            return topTeachers;
        }

        public async Task<SignInStudentObject> SignInStudent(int telegramId)
        {
            var activityUpdate = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (activityUpdate != null)
            {
                activityUpdate.LastActivity = 1;
                _botDbContext.SaveChangesAsync();
            }


            var topTeachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeCategory)
                .Where(t => t.Visibility != false && t.Block != true)
                .OrderByDescending(e => e.PaymentRating)
                .ThenByDescending(e => e.Rating)
                .Take(5)
                .Select(t => new BriefTeacherObject
                    {
                        User = t.User,
                        Image = t.Image,
                        Status = t.Status,
                        Science = t.Science,
                        LessonTarget = t.LessonTarget,
                        AgeCategory = t.AgeCategory,
                        Experience = t.Experience,
                        AboutMe = t.AboutMe,
                        Price = t.Price,
                        Rating = t.Rating
                    })
                .ToListAsync();

            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            var usefulLinks = await _botDbContext.StudentUseFulUrls.ToListAsync();



            DateTime twoHoursAgo = DateTime.UtcNow.AddHours(-2);

            var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var studentId = await _botDbContext.Students
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();


            List<int> orderIdsList = await _botDbContext.Orders
                .Where(t => t.StudentId == studentId)
                .Where(o => o.DateTimeAccept.HasValue && o.DateTimeAccept.Value <= twoHoursAgo)
                .Select(o => o.Id)
                .ToListAsync();


            List<OrderSurveyDetails> ordersSurveyList = await _botDbContext.Surveis
                .Include(s => s.Order)
                    .ThenInclude(o => o.Teacher)
                    .ThenInclude(t => t.User)
                .Where(s => orderIdsList.Contains(s.OrderId) && !s.StudentAnswer)
                .Select(s => new OrderSurveyDetails
                {
                    OrderId = s.OrderId,
                    Name = s.Order.Teacher.User.Name,
                    SecondName = s.Order.Teacher.User.SecondName,
                    TelegramName = s.Order.Teacher.User.TelegramName
                })
                .ToListAsync();


            var surveyStatus = ordersSurveyList.Any();


            var signIn = new SignInStudentObject()
            {
                Name = user?.Name,
                Teachers = topTeachers,
                UsefulLinks = usefulLinks,
                SurveyStatus = surveyStatus,
                OrdersSurvey = ordersSurveyList,
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
