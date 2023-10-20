﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Teacher> FullTeacher(int telegramId)
        {
            return await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeСategory)
                .FirstOrDefaultAsync(u => u.User.TelegramId == telegramId);
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
                TelegramName = userSignUpStudent.TelegramName,
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

        public async Task<bool> UserSignUpTeacher(UserSignUpTeacherObject userSignUpTeacherObject)
        {
            var newUser = new User()
            {
                TelegramId = userSignUpTeacherObject.TelegramId,
                TelegramName =userSignUpTeacherObject.TelegramName,
                Name = userSignUpTeacherObject.Name,
                SecondName = userSignUpTeacherObject.SecondName,
                LastActivity = 2
            };
            _botDbContext.Users.Add(newUser);
            await _botDbContext.SaveChangesAsync();


            var newTeacher = new Teacher()
            {
                UserId = newUser.Id,
                Visibility = false,
                Block = false,
                Rating = 5,
                PaymentRating = 400
            };

            _botDbContext.Teachers.Add(newTeacher);
            await _botDbContext.SaveChangesAsync();


            var check = await _botDbContext.Users.FirstOrDefaultAsync(c => c.TelegramId == userSignUpTeacherObject.TelegramId);
            if (check == null)
                return false;
            else
                return true;
        }
    }
}
