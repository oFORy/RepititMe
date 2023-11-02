using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Teachers;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly BotDbContext _botDbContext;
        public UserRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public async Task<Teacher> FullTeacher(long telegramId)
        {
            return await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeCategory)
                .FirstOrDefaultAsync(u => u.User.TelegramId == telegramId);
        }


        public async Task<Dictionary<string, int>> UserAccessId(long telegramId)
        {
            var result = new Dictionary<string, int>
                {
                    { "LastActivity", 0 },
                    { "Student", 0 },
                    { "Teacher", 0 }
                };

            var user = await _botDbContext.Users.FirstOrDefaultAsync(s => s.TelegramId == telegramId);
            if (user == null)
            {
                return result;
            }

            result["LastActivity"] = user.LastActivity;

            var isStudent = await _botDbContext.Students.AnyAsync(s => s.UserId == user.Id);
            var isTeacher = await _botDbContext.Teachers.AnyAsync(t => t.UserId == user.Id);

            result["Student"] = isStudent ? 1 : 0;
            result["Teacher"] = isTeacher ? 1 : 0;

            return result;
        }

        public async Task<bool> UserSignUpStudent(UserSignUpStudentObject userSignUpStudent)
        {
            var already = await _botDbContext.Users.FirstOrDefaultAsync(t => t.TelegramId == userSignUpStudent.TelegramId);

            if (already == null)
            {
                var newUser = new User()
                {
                    TelegramId = userSignUpStudent.TelegramId,
                    TelegramName = userSignUpStudent.TelegramName,
                    Name = userSignUpStudent.Name,
                    LastActivity = 1
                };
                await _botDbContext.Users.AddAsync(newUser);
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                var newStudent = new Student()
                {
                    UserId = newUser.Id
                };
                await _botDbContext.Students.AddAsync(newStudent);
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
            {
                already.Name = userSignUpStudent.Name;
                already.LastActivity = 1;
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                var newStudent = new Student()
                {
                    UserId = already.Id
                };
                await _botDbContext.Students.AddAsync(newStudent);

                return await _botDbContext.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UserSignUpTeacher(UserSignUpTeacherObject userSignUpTeacherObject)
        {
            var already = await _botDbContext.Users.FirstOrDefaultAsync(t => t.TelegramId == userSignUpTeacherObject.TelegramId);
            if (already == null)
            {
                var newUser = new User()
                {
                    TelegramId = userSignUpTeacherObject.TelegramId,
                    TelegramName = userSignUpTeacherObject.TelegramName,
                    Name = userSignUpTeacherObject.Name,
                    SecondName = userSignUpTeacherObject.SecondName,
                    LastActivity = 2,
                    Block = false,
                };

                await _botDbContext.Users.AddAsync(newUser);
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                var newTeacher = new Teacher()
                {
                    UserId = newUser.Id,
                    Visibility = false,
                    Rating = 5,
                    PaymentRating = 0
                };

                await _botDbContext.Teachers.AddAsync(newTeacher);
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            else
            {
                already.Name = userSignUpTeacherObject.Name;
                already.SecondName = userSignUpTeacherObject.SecondName;
                already.LastActivity = 2;
                if (await _botDbContext.SaveChangesAsync() == 0)
                    return false;

                var newTeacher = new Teacher()
                {
                    UserId = already.Id,
                    Visibility = false,
                    Rating = 5,
                    PaymentRating = 0
                };

                await _botDbContext.Teachers.AddAsync(newTeacher);
                return await _botDbContext.SaveChangesAsync() > 0;
            }

            
        }
    }
}
