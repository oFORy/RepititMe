using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Surveis.Common;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Surveis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly BotDbContext _botDbContext;
        public SurveyRepository(BotDbContext botDbContext)
        {
            _botDbContext = botDbContext;
        }


        public async Task<bool> SurveyStudentFirst(SurveyStudentFirstObject surveyStudentObject)
        {
            if (surveyStudentObject == null)
                throw new ArgumentNullException(nameof(surveyStudentObject));

            var survey = await _botDbContext.SurveisFirst
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyStudentObject.TelegramId &&
                                          s.OrderId == surveyStudentObject.OrderId);


            if (survey.TeacherAccept == true)
            {

            }

            if (survey != null)
            {
                survey.StudentAccept = surveyStudentObject.StudentAccept;
                survey.StudentPrice = surveyStudentObject.StudentPrice;
                survey.StudentWhy = surveyStudentObject.StudentWhy;
                survey.StudentAnswer = true;
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                var order = await _botDbContext.Orders
                    .Include(o => o.Student)
                        .ThenInclude(s => s.User)
                    .Include(o => o.Teacher)
                        .ThenInclude(t => t.User)
                    .FirstOrDefaultAsync(o => o.Id == surveyStudentObject.OrderId);

                var newSecondSurvey = new SurveySecond()
                {
                    TelegramIdStudent = order.Student.User.TelegramId,
                    TelegramIdTeacher = order.Teacher.User.TelegramId,
                    OrderId = surveyStudentObject.OrderId,
                    StudentAnswer = false,
                    TeacherAnswer = false
                };

                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                return true;

            }
            else
                return false;

        }

        public async Task<bool> SurveyStudentSecond(SurveyStudentSecondObject surveyStudentSecondObject)
        {
            if (surveyStudentSecondObject == null)
                throw new ArgumentNullException(nameof(surveyStudentSecondObject));

            var survey = await _botDbContext.SurveisSecond
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyStudentSecondObject.TelegramId &&
                                          s.OrderId == surveyStudentSecondObject.OrderId);

            if (survey != null)
            {
                survey.StudentAccept = surveyStudentSecondObject.StudentAccept;
                survey.StudentCancel = surveyStudentSecondObject.StudentCancel;
                survey.StudentAnswer = true;

                if (surveyStudentSecondObject.RepitSurveyStudent != null)
                {
                    survey.RepitSurveyStudent = DateTime.UtcNow.AddDays(+3);
                    survey.StudentAnswer = false;
                }

                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }

        public async Task<bool> SurveyTeacherFirst(SurveyTeacherFirstObject surveyTeacherObject)
        {

            if (surveyTeacherObject == null)
                throw new ArgumentNullException(nameof(surveyTeacherObject));

            var survey = await _botDbContext.SurveisFirst
                .FirstOrDefaultAsync(s => s.TelegramIdTeacher == surveyTeacherObject.TelegramId &&
                                          s.OrderId == surveyTeacherObject.OrderId);

            if (surveyTeacherObject.DateTimeFirstLesson != null)
            {
                var orderUpdate = await _botDbContext.Orders
                .Where(o => o.Id == surveyTeacherObject.OrderId)
                .FirstOrDefaultAsync();

                if (orderUpdate != null)
                {
                    orderUpdate.DateTimeFirstLesson = surveyTeacherObject.DateTimeFirstLesson;
                    await _botDbContext.SaveChangesAsync();
                }
                else
                    return false;
            }
            
            if (survey != null)
            {
                survey.TeacherAccept = surveyTeacherObject.TeacherAccept;
                survey.TeacherPrice = surveyTeacherObject.TeacherPrice;
                survey.TeacherCause = surveyTeacherObject.TeacherCause;
                survey.TeacherSpecify = surveyTeacherObject.TeacherSpecify;
                survey.TeacherWhy = surveyTeacherObject.TeacherWhy;
                survey.TeacherAnswer = true;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }

        public async Task<bool> SurveyTeacherSecond(SurveyTeacherSecondObject surveyTeacherSecondObject)
        {
            if (surveyTeacherSecondObject == null)
                throw new ArgumentNullException(nameof(surveyTeacherSecondObject));

            var survey = await _botDbContext.SurveisSecond
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyTeacherSecondObject.TelegramId &&
                                          s.OrderId == surveyTeacherSecondObject.OrderId);

            if (survey != null)
            {
                survey.TeacherAccept = surveyTeacherSecondObject.TeacherAccept;
                survey.TeacherCancel = surveyTeacherSecondObject.TeacherCancel;
                survey.TeacherAnswer = true;

                if (surveyTeacherSecondObject.RepitSurveyTeacher != null)
                {
                    survey.RepitSurveyTeacher = DateTime.UtcNow.AddDays(+3);
                    survey.TeacherAnswer = false;
                }
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;
        }
    }
}
