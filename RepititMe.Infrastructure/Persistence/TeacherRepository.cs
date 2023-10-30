using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Teachers.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Students;
using RepititMe.Domain.Object.Teachers;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RepititMe.Infrastructure.Persistence
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly BotDbContext _botDbContext;
        public TeacherRepository(BotDbContext context)
        {
            _botDbContext = context;
        }


        public async Task<bool> UpdateTeacherDataFolder(UpdateTeacherDataFolderObject updateTeacherDataFolderObject)
        {
            var teacher = await _botDbContext.Teachers.FirstOrDefaultAsync(s => s.UserId == updateTeacherDataFolderObject.UserId);
            if (teacher != null)
            {
                teacher.Image = updateTeacherDataFolderObject?.Image;
                teacher.Certificates = updateTeacherDataFolderObject?.Certificates;
                teacher.VideoPresentation = updateTeacherDataFolderObject?.VideoPresentation;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }

        public async Task<int> ChangeProfile(Teacher updatedTeacher, int telegramId)
        {
            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (user != null)
            {
                var teacher = await _botDbContext.Teachers.FirstOrDefaultAsync(t => t.UserId == user.Id);

                if (teacher == null)
                {
                    return -1;
                }
                else
                {
                    teacher.PaymentRating = 0;
                    teacher.Image = updatedTeacher.Image;
                    teacher.StatusId = updatedTeacher.StatusId;
                    teacher.ScienceId = updatedTeacher.ScienceId;
                    teacher.LessonTargetId = updatedTeacher.LessonTargetId;
                    teacher.AgeCategoryId = updatedTeacher.AgeCategoryId;
                    teacher.Experience = updatedTeacher.Experience;
                    teacher.AboutMe = updatedTeacher.AboutMe;
                    teacher.Price = updatedTeacher.Price;
                    teacher.VideoPresentation = updatedTeacher.VideoPresentation;
                    teacher.Certificates = updatedTeacher.Certificates;

                    if (updatedTeacher.Image != null)
                        teacher.PaymentRating += 200;
                    if (updatedTeacher.VideoPresentation != null)
                        teacher.PaymentRating += 400;
                    if (updatedTeacher.Certificates != null && updatedTeacher.Certificates.Any())
                        teacher.PaymentRating += updatedTeacher.Certificates.Count * 50;
                    if (updatedTeacher.StatusId != null && updatedTeacher.ScienceId != null && updatedTeacher.LessonTargetId != null && updatedTeacher.AgeCategoryId != null && updatedTeacher.Experience != null && updatedTeacher.AboutMe != null)
                        teacher.PaymentRating += 300;

                    if (await _botDbContext.SaveChangesAsync() == 0)
                        return -1;
                }
                return user.Id;
            }

            return -1;

        }

        public async Task<bool> ChangeVisability(int telegramId)
        {
            var teacher = await _botDbContext.Teachers
                     .Include(t => t.User)
                     .FirstOrDefaultAsync(t => t.User.TelegramId == telegramId);

            if (teacher != null)
            {
                teacher.Visibility = !teacher.Visibility;
                return await _botDbContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<SignInTeacherObject> SignInTeacher(int telegramId)
        {
            var activityUpdate = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (activityUpdate != null)
            {
                activityUpdate.LastActivity = 2;
                await _botDbContext.SaveChangesAsync();
            }

            DateTime twoHoursAgo = DateTime.UtcNow.AddHours(-2);

            /*var userId = await _botDbContext.Users
                .Where(u => u.TelegramId == telegramId)
                .Select(u => u.Id)
                .SingleOrDefaultAsync();

            var teacherId = await _botDbContext.Teachers
                .Where(s => s.UserId == userId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();*/


            var teacherId = await _botDbContext.Teachers
                .Include(u => u.User)
                .Where(s => s.User.TelegramId == telegramId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();


            List<int> orderIdsListFirst = await _botDbContext.Orders
                .Where(t => t.TeacherId == teacherId)
                .Where(o => o.DateTimeAccept.HasValue && o.DateTimeAccept.Value <= twoHoursAgo)
                .Select(o => o.Id)
                .ToListAsync();


            /*List<int> ordersSurveyListFirst = await _botDbContext.SurveisFirst
                .Where(s => orderIdsListFirst.Contains(s.OrderId) && !s.TeacherAnswer)
                .Select(s => s.OrderId)
                .ToListAsync();*/


            List<OrderSurveyDetailsTeacher> ordersSurveyListFirst = await _botDbContext.SurveisFirst
                .Include(s => s.Order)
                    .ThenInclude(o => o.Student)
                    .ThenInclude(t => t.User)
                .Where(s => orderIdsListFirst.Contains(s.OrderId) && !s.TeacherAnswer)
                .Select(s => new OrderSurveyDetailsTeacher
                {
                    OrderId = s.OrderId,
                    Name = s.Order.Student.User.Name,
                    TelegramName = s.Order.Student.User.TelegramName
                })
                .ToListAsync();


            List<int> orderIdsListSecond = await _botDbContext.Orders
                .Where(t => t.TeacherId == teacherId)
                .Where(o => o.DateTimeAccept.HasValue && o.DateTimeAccept.Value.Date <= DateTime.UtcNow.Date && DateTime.UtcNow.TimeOfDay >= new TimeSpan(21, 0, 0))
                .Select(o => o.Id)
                .ToListAsync();

            List<OrderSurveyDetailsTeacher> ordersSurveyListSecond = await _botDbContext.SurveisSecond
                .Include(s => s.Order)
                    .ThenInclude(o => o.Student)
                    .ThenInclude(t => t.User)
                .Where(s => orderIdsListSecond.Contains(s.OrderId) && !s.StudentAnswer)
                .Select(s => new OrderSurveyDetailsTeacher
                {
                    OrderId = s.OrderId,
                    Name = s.Order.Student.User.Name,
                    TelegramName = s.Order.Student.User.TelegramName
                })
                .ToListAsync();




            var signIn = new SignInTeacherObject()
            {
                TeacherIn = await _botDbContext.Teachers.Include(u => u.User).FirstOrDefaultAsync(u => u.User.TelegramId == telegramId),
                UsefulLinks = await _botDbContext.TeacherUseFulUrls.ToListAsync(),
                SurveyStatusFirst = ordersSurveyListFirst.Any(),
                OrdersSurveyFirst = ordersSurveyListFirst,
                SurveyStatusSecond = ordersSurveyListSecond.Any(),
                OrdersSurveySecond = ordersSurveyListSecond
            }; 

            return signIn;
        }

        public async Task<bool> SignOutTeacher(int telegramId)
        {
            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (user != null)
            {
                user.LastActivity = 0;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
