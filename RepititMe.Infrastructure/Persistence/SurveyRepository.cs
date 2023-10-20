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


        public async Task<bool> SurveyStudent(SurveyStudentObject surveyStudentObject)
        {

            if (surveyStudentObject == null)
                throw new ArgumentNullException(nameof(surveyStudentObject));

            var survey = await _botDbContext.Surveis
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyStudentObject.TelegramId &&
                                          s.OrderId == surveyStudentObject.OrderId);

            if (survey != null)
            {
                survey.StudentAccept = surveyStudentObject.StudentAccept;
                survey.StudentPrice = surveyStudentObject.StudentPrice;
                survey.StudentWhy = surveyStudentObject.StudentWhy;
                survey.StudentAnswer = true;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
                return false;

        }

        public async Task<bool> SurveyTeacher(SurveyTeacherObject surveyTeacherObject)
        {

            if (surveyTeacherObject == null)
                throw new ArgumentNullException(nameof(surveyTeacherObject));

            var survey = await _botDbContext.Surveis
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
    }
}
