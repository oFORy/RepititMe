using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
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

        public async Task<Teacher> FullTeacher(int userId)
        {
            return await _botDbContext.Teachers.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> UpdateTeacherDataFolder(UpdateTeacherDataFolderObject updateTeacherDataFolderObject)
        {
            var teacher = await _botDbContext.Teachers.FirstOrDefaultAsync(s => s.UserId == updateTeacherDataFolderObject.UserId);
            if (teacher != null)
            {
                teacher.Image = updateTeacherDataFolderObject?.Image;
                teacher.Certificates = updateTeacherDataFolderObject?.Certificates;
                teacher.VideoPresentation = updateTeacherDataFolderObject?.VideoPresentation;
                await _botDbContext.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<Dictionary<string, int>> UserAccessId(int telegramId)
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
            var newUser = new User()
            {
                TelegramId = userSignUpStudent.TelegramId,
                Name = userSignUpStudent.Name,
                LastActivity = 1
            };
            _botDbContext.Users.Add(newUser);
            _botDbContext.SaveChanges();

            var newStudent = new Student()
            {
                UserId = newUser.Id
            };
            _botDbContext.Students.Add(newStudent);
            _botDbContext.SaveChanges();

            var check = await _botDbContext.Users.FirstOrDefaultAsync(c => c.TelegramId == userSignUpStudent.TelegramId);

            if (check == null)
                return false;
            else
                return true;
        }

        public async Task<int> UserSignUpTeacher(Teacher teacher, string name, string secondName, int telegramId)
        {
            var newUser = new User()
            {
                TelegramId = telegramId,
                Name = name,
                SecondName = secondName,
                LastActivity = 2
            };
            _botDbContext.Users.Add(newUser);
            await _botDbContext.SaveChangesAsync();

            teacher.UserId = newUser.Id;
            _botDbContext.Teachers.Add(teacher);
            await _botDbContext.SaveChangesAsync();
            return newUser.Id;
        }
    }
}
