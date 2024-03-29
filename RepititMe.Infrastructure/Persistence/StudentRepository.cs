﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ChangeProfile(long telegramId, string name)
        {
            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (user != null)
            {
                user.Name = name;
                return await _botDbContext.SaveChangesAsync() > 0;
            }

            return false;
        }


        public async Task<List<BriefTeacherObject>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject)
        {
            /*var topTeachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.TeacherLessonTargets)
                    .ThenInclude(tlt => tlt.LessonTarget)
                .Include(u => u.TeacherAgeCategories)
                    .ThenInclude(tac => tac.AgeCategory)
                .Where(t => t.Visibility != false
                            && !t.User.Block
                            && t.ScienceId == searchCategoriesResultObject.ScienceId
                            && t.TeacherLessonTargets.Any(lt => lt.LessonTargetId == searchCategoriesResultObject.LessonTargetId)
                            && t.TeacherAgeCategories.Any(ac => ac.AgeCategoryId == searchCategoriesResultObject.AgeCategoryId)
                            && searchCategoriesResultObject.StatusId.Contains((int)t.StatusId)
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
                    LessonTarget = t.TeacherLessonTargets.Select(tlt => tlt.LessonTarget).ToList(),
                    AgeCategory = t.TeacherAgeCategories.Select(tac => tac.AgeCategory).ToList(),
                    Experience = t.Experience,
                    AboutMe = t.AboutMe,
                    Price = t.Price,
                    Rating = t.Rating
                })
                .ToListAsync();

            return topTeachers;*/

            var query = _botDbContext.Teachers
                        .Include(u => u.User)
                        .Include(u => u.Status)
                        .Include(u => u.Science)
                        .Include(u => u.TeacherLessonTargets)
                            .ThenInclude(tlt => tlt.LessonTarget)
                        .Include(u => u.TeacherAgeCategories)
                            .ThenInclude(tac => tac.AgeCategory)
                        .Where(t => t.Visibility != false
                                    && !t.User.Block
                                    && t.ScienceId == searchCategoriesResultObject.ScienceId
                                    && t.TeacherLessonTargets.Any(lt => lt.LessonTargetId == searchCategoriesResultObject.LessonTargetId)
                                    && t.TeacherAgeCategories.Any(ac => ac.AgeCategoryId == searchCategoriesResultObject.AgeCategoryId)
                                    && searchCategoriesResultObject.StatusId.Contains((int)t.StatusId));

            if (searchCategoriesResultObject.LowPrice.HasValue)
            {
                query = query.Where(t => t.Price >= searchCategoriesResultObject.LowPrice.Value);
            }

            if (searchCategoriesResultObject.HighPrice.HasValue)
            {
                query = query.Where(t => t.Price <= searchCategoriesResultObject.HighPrice.Value);
            }

            var topTeachers = await query
                            .OrderByDescending(e => e.Rating)
                            .ThenByDescending(e => e.PaymentRating)
                            .Take(5)
                            .Select(t => new BriefTeacherObject
                            {
                                User = t.User,
                                Image = t.Image,
                                Status = t.Status,
                                Science = t.Science,
                                LessonTarget = t.TeacherLessonTargets.Select(tlt => tlt.LessonTarget).ToList(),
                                AgeCategory = t.TeacherAgeCategories.Select(tac => tac.AgeCategory).ToList(),
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
                AgeCategories = await _botDbContext.AgeCategories.OrderBy(x => x.Name).ToListAsync(),
                Sciences = await _botDbContext.Sciences
                    .OrderBy(x => x.Name)
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


        public async Task<List<BriefTeacherObject>> ShowTeachers(ShowTeachersFilterObject showTeachersFilterObject)
        {
            if (showTeachersFilterObject.ScienceId == null)
            {
                return await _botDbContext.Teachers
                    .Include(u => u.User)
                    .Include(u => u.Status)
                    .Include(u => u.Science)
                    .Include(u => u.TeacherLessonTargets)
                        .ThenInclude(tlt => tlt.LessonTarget)
                    .Include(u => u.TeacherAgeCategories)
                        .ThenInclude(tac => tac.AgeCategory)
                    .Where(t => t.Visibility != false && !t.User.Block && !showTeachersFilterObject.WasTeachers.Contains(t.User.TelegramId))
                    .OrderByDescending(e => e.PaymentRating)
                    .ThenByDescending(e => e.Rating)
                    .Take(5)
                    .Select(t => new BriefTeacherObject
                    {
                        User = t.User,
                        Image = t.Image,
                        Status = t.Status,
                        Science = t.Science,
                        LessonTarget = t.TeacherLessonTargets.Select(tlt => tlt.LessonTarget).ToList(),
                        AgeCategory = t.TeacherAgeCategories.Select(tac => tac.AgeCategory).ToList(),
                        Experience = t.Experience,
                        AboutMe = t.AboutMe,
                        Price = t.Price,
                        Rating = t.Rating
                    })
                    .ToListAsync();
            }
            else
            {
                var query = _botDbContext.Teachers
                    .Include(u => u.User)
                    .Include(u => u.Status)
                    .Include(u => u.Science)
                    .Include(u => u.TeacherLessonTargets)
                        .ThenInclude(tlt => tlt.LessonTarget)
                    .Include(u => u.TeacherAgeCategories)
                        .ThenInclude(tac => tac.AgeCategory)
                    .Where(t => t.Visibility != false && !t.User.Block && !showTeachersFilterObject.WasTeachers.Contains(t.User.TelegramId));

                if (showTeachersFilterObject.ScienceId != null)
                {
                    query = query.Where(t => t.ScienceId == showTeachersFilterObject.ScienceId);
                }
                if (showTeachersFilterObject.PriceLow != null)
                {
                    query = query.Where(t => t.Price >= showTeachersFilterObject.PriceLow);
                }
                if (showTeachersFilterObject.PriceHigh != null)
                {
                    query = query.Where(t => t.Price <= showTeachersFilterObject.PriceHigh);
                }
                if (showTeachersFilterObject.PriceHigh != null && showTeachersFilterObject.PriceLow != null)
                {
                    query = query.Where(t => t.Price <= showTeachersFilterObject.PriceHigh && t.Price >= showTeachersFilterObject.PriceLow);
                }
                if (showTeachersFilterObject.TeacherStatusId != null)
                {
                    query = query.Where(t => t.StatusId == showTeachersFilterObject.TeacherStatusId);
                }
                if (showTeachersFilterObject.LessonTargetId != null)
                {
                    query = query.Where(t => t.TeacherLessonTargets.Any(ltId => ltId.LessonTargetId == showTeachersFilterObject.LessonTargetId));
                }

                var result = await query
                    .OrderByDescending(e => e.PaymentRating)
                    .ThenByDescending(e => e.Rating)
                    .Take(5)
                    .Select(t => new BriefTeacherObject
                    {
                        User = t.User,
                        Image = t.Image,
                        Status = t.Status,
                        Science = t.Science,
                        LessonTarget = t.TeacherLessonTargets.Select(tlt => tlt.LessonTarget).ToList(),
                        AgeCategory = t.TeacherAgeCategories.Select(tac => tac.AgeCategory).ToList(),
                        Experience = t.Experience,
                        AboutMe = t.AboutMe,
                        Price = t.Price,
                        Rating = t.Rating
                    })
                    .ToListAsync();

                return result;
            }


        }




        public async Task<SignInStudentObject> SignInStudent(long telegramId)
        {
            var activityUpdate = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (activityUpdate != null)
            {
                activityUpdate.LastActivity = 1;
                await _botDbContext.SaveChangesAsync();
            }

            var user = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            DateTime twoHoursAgo = DateTime.UtcNow.AddHours(-2);

            DateTime now = DateTime.UtcNow.AddHours(3);

            var studentId = await _botDbContext.Students
                .Include(u => u.User)
                .Where(s => s.User.TelegramId == telegramId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync();


            List<int> orderIdsListFirst = await _botDbContext.Orders
                .Where(t => t.StudentId == studentId)
                .Where(o => o.DateTimeAccept.HasValue && o.DateTimeAccept.Value <= twoHoursAgo)
                .Select(o => o.Id)
                .ToListAsync();


            List<OrderSurveyDetailsStudent> ordersSurveyListFirst = await _botDbContext.SurveisFirst
                .Include(s => s.Order)
                    .ThenInclude(o => o.Teacher)
                    .ThenInclude(t => t.User)
                .Where(s => orderIdsListFirst.Contains(s.OrderId) && !s.StudentAnswer)
                .Select(s => new OrderSurveyDetailsStudent
                {
                    OrderId = s.OrderId,
                    Name = s.Order.Teacher.User.Name,
                    SecondName = s.Order.Teacher.User.SecondName,
                    TelegramName = s.Order.Teacher.User.TelegramName
                })
                .ToListAsync();


            List<int> orderIdsListSecond = await _botDbContext.Orders
                .Where(t => t.StudentId == studentId)
                .Where(o => o.DateTimeFirstLesson.HasValue && ((o.DateTimeFirstLesson.Value.Date == now.Date && now.Hour >= 21) || (o.DateTimeFirstLesson.Value.Date < now.Date)))
                .Select(o => o.Id)
                .ToListAsync();


            List<OrderSurveyDetailsStudent> ordersSurveyListSecond = await _botDbContext.SurveisSecond
                .Include(s => s.Order)
                    .ThenInclude(o => o.Teacher)
                    .ThenInclude(t => t.User)
                .Where(s => orderIdsListSecond.Contains(s.OrderId) && (s.RepitSurveyStudent != null ? (((s.RepitSurveyStudent.Value.Date == now.Date && now.Hour >= 21) || (s.RepitSurveyStudent.Value.Date < now.Date)) && !s.StudentAnswer) : (((s.Order.DateTimeFirstLesson == now.Date && now.Hour >= 21) || (s.Order.DateTimeFirstLesson < now.Date)) && !s.StudentAnswer)))
                .Select(s => new OrderSurveyDetailsStudent
                {
                    OrderId = s.OrderId,
                    Name = s.Order.Teacher.User.Name,
                    SecondName = s.Order.Teacher.User.SecondName,
                    TelegramName = s.Order.Teacher.User.TelegramName
                })
                .ToListAsync();


            var signIn = new SignInStudentObject()
            {
                Name = user?.Name,
                Blocked = user.Block,
                UsefulLinks = await _botDbContext.StudentUseFulUrls.ToListAsync(),
                SurveyStatusFirst = ordersSurveyListFirst.Any(),
                OrdersSurveyFirst = ordersSurveyListFirst,
                SurveyStatusSecond = ordersSurveyListSecond.Any(),
                OrdersSurveySecond = ordersSurveyListSecond
            };

            return signIn;
        }

        public async Task<bool> SignOutStudent(long telegramId)
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
