using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Surveis.Common;
using RepititMe.Domain.Entities.Users;
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
                await _botDbContext.SurveisSecond.AddAsync(newSecondSurvey);

                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;




                var surveyCheck = await _botDbContext.SurveisFirst
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyStudentObject.TelegramId &&
                                          s.OrderId == surveyStudentObject.OrderId);

                if (surveyCheck != null && surveyCheck.TeacherAnswer)
                {
                    if (surveyCheck.StudentAccept != surveyCheck.TeacherAccept)
                    {
                        var newDispute = new Dispute()
                        {
                            TeacherId = survey.TelegramIdTeacher,
                            StudentId = surveyStudentObject.TelegramId,
                            AcceptFromStudent = surveyCheck.StudentAccept,
                            AcceptFromTeacher = surveyCheck.TeacherAccept,
                            DataFromTeacher = surveyCheck.TeacherCause ?? surveyCheck.TeacherSpecify ?? surveyCheck.TeacherWhy,
                            DataFromStudent = surveyCheck.StudentWhy
                        };
                        await _botDbContext.Disputes.AddAsync(newDispute);
                        if (await _botDbContext.SaveChangesAsync() == 0)
                            return false;

                    }
                    else if ((survey.StudentPrice != null && survey.TeacherPrice != null) && (surveyCheck.StudentPrice != surveyCheck.TeacherPrice))
                    {
                        var newDispute = new Dispute()
                        {
                            TeacherId = surveyCheck.TelegramIdTeacher,
                            StudentId = surveyStudentObject.TelegramId,
                            PriceTeacher = surveyCheck.TeacherPrice,
                            PriceStudent = surveyCheck.StudentPrice
                        };
                        await _botDbContext.Disputes.AddAsync(newDispute);
                        if (await _botDbContext.SaveChangesAsync() == 0)
                            return false;
                    }
                }
                
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

                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;



                var surveyCheck = await _botDbContext.SurveisSecond
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyStudentSecondObject.TelegramId &&
                                          s.OrderId == surveyStudentSecondObject.OrderId);

                if (surveyCheck != null && surveyCheck.TeacherAnswer)
                {
                    if (surveyCheck.StudentAccept != surveyCheck.TeacherAccept || 
                        surveyCheck.StudentAccept == surveyCheck.TeacherAccept == false || 
                        surveyCheck.StudentCancel != surveyCheck.TeacherCancel || 
                        surveyCheck.StudentWannaNext != surveyCheck.TeacherWannaNext)
                    {
                        var newDispute = new Dispute()
                        {
                            TeacherId = surveyCheck.TelegramIdTeacher,
                            StudentId = surveyStudentSecondObject.TelegramId,
                            AcceptFromStudent = surveyCheck.StudentAccept,
                            AcceptFromTeacher = surveyCheck.TeacherAccept,
                            DataFromTeacher = surveyCheck.TeacherCause ??
                                surveyCheck.TeacherSpecify ?? 
                                surveyCheck.TeacherWhy ?? 
                                (surveyCheck.StudentCancel != surveyCheck.TeacherCancel ? $"Ученик {((bool)surveyCheck.StudentCancel ? "не планирует занятия" : "не выбрал данную опцию")} | Преподаватель {((bool)surveyCheck.TeacherCancel ? "не планирует занятия" : "не выбрал данную опцию")}" : null) ??
                                (surveyCheck.StudentWannaNext != surveyCheck.TeacherWannaNext ? $"Ученик {((bool)surveyCheck.StudentWannaNext ? "не планирует занятия" : "не выбрал данную опцию")} | Преподаватель {((bool)surveyCheck.TeacherWannaNext ? "не планирует занятия" : "не выбрал данную опцию")}" : null),
                            DataFromStudent = surveyCheck.StudentWhy != null ? surveyCheck.StudentWhy : null
                        };
                        await _botDbContext.Disputes.AddAsync(newDispute);
                        if (await _botDbContext.SaveChangesAsync() == 0)
                            return false;
                    }
                }

                return true;
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
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;


                var surveyCheck = await _botDbContext.SurveisFirst
                .FirstOrDefaultAsync(s => s.TelegramIdTeacher == surveyTeacherObject.TelegramId &&
                                          s.OrderId == surveyTeacherObject.OrderId);

                if (surveyCheck != null && surveyCheck.StudentAnswer)
                {
                    if (surveyCheck.StudentAccept != surveyCheck.TeacherAccept)
                    {
                        var newDispute = new Dispute()
                        {
                            TeacherId = survey.TelegramIdTeacher,
                            StudentId = surveyTeacherObject.TelegramId,
                            AcceptFromStudent = surveyCheck.StudentAccept,
                            AcceptFromTeacher = surveyCheck.TeacherAccept,
                            DataFromTeacher = surveyCheck.TeacherCause ?? surveyCheck.TeacherSpecify ?? surveyCheck.TeacherWhy,
                            DataFromStudent = surveyCheck.StudentWhy
                        };
                        await _botDbContext.Disputes.AddAsync(newDispute);
                        if (await _botDbContext.SaveChangesAsync() == 0)
                            return false;
                    }
                    else if ((survey.StudentPrice != null && survey.TeacherPrice != null) && (surveyCheck.StudentPrice != surveyCheck.TeacherPrice))
                    {
                        var newDispute = new Dispute()
                        {
                            TeacherId = surveyCheck.TelegramIdTeacher,
                            StudentId = surveyTeacherObject.TelegramId,
                            PriceTeacher = surveyCheck.TeacherPrice,
                            PriceStudent = surveyCheck.StudentPrice
                        };
                        await _botDbContext.Disputes.AddAsync(newDispute);
                        if (await _botDbContext.SaveChangesAsync() == 0)
                            return false;
                    }
                }

                return true;
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
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                var surveyCheck = await _botDbContext.SurveisSecond
                .FirstOrDefaultAsync(s => s.TelegramIdStudent == surveyTeacherSecondObject.TelegramId &&
                                          s.OrderId == surveyTeacherSecondObject.OrderId);

                if (surveyCheck != null && surveyCheck.StudentAnswer)
                {
                    if (surveyCheck.StudentAccept != surveyCheck.TeacherAccept ||
                        surveyCheck.StudentAccept == surveyCheck.TeacherAccept == false ||
                        surveyCheck.StudentCancel != surveyCheck.TeacherCancel ||
                        surveyCheck.StudentWannaNext != surveyCheck.TeacherWannaNext)
                    {
                        var newDispute = new Dispute()
                        {
                            TeacherId = surveyCheck.TelegramIdTeacher,
                            StudentId = surveyTeacherSecondObject.TelegramId,
                            AcceptFromStudent = surveyCheck.StudentAccept,
                            AcceptFromTeacher = surveyCheck.TeacherAccept,
                            DataFromTeacher = surveyCheck.TeacherCause ??
                                surveyCheck.TeacherSpecify ??
                                surveyCheck.TeacherWhy ??
                                (surveyCheck.StudentCancel != surveyCheck.TeacherCancel ? $"Ученик {((bool)surveyCheck.StudentCancel ? "не планирует занятия" : "не выбрал данную опцию")} | Преподаватель {((bool)surveyCheck.TeacherCancel ? "не планирует занятия" : "не выбрал данную опцию")}" : null) ??
                                (surveyCheck.StudentWannaNext != surveyCheck.TeacherWannaNext ? $"Ученик {((bool)surveyCheck.StudentWannaNext ? "не планирует занятия" : "не выбрал данную опцию")} | Преподаватель {((bool)surveyCheck.TeacherWannaNext ? "не планирует занятия" : "не выбрал данную опцию")}" : null),
                            DataFromStudent = surveyCheck.StudentWhy != null ? surveyCheck.StudentWhy : null
                        };
                        await _botDbContext.Disputes.AddAsync(newDispute);
                        if (await _botDbContext.SaveChangesAsync() == 0)
                            return false;
                    }
                }

                return true;


            }
            else
                return false;
        }
    }
}
