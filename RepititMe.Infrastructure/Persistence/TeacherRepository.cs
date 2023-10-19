using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Teachers.Common;
using RepititMe.Domain.Entities.Users;
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
                await _botDbContext.SaveChangesAsync();
                return true;
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
                    teacher.Image = updatedTeacher.Image;
                    teacher.StatusId = updatedTeacher.StatusId;
                    teacher.ScienceId = updatedTeacher.ScienceId;
                    teacher.LessonTargetId = updatedTeacher.LessonTargetId;
                    teacher.AgeСategoryId = updatedTeacher.AgeСategoryId;
                    teacher.Experience = updatedTeacher.Experience;
                    teacher.AboutMe = updatedTeacher.AboutMe;
                    teacher.Price = updatedTeacher.Price;
                    teacher.VideoPresentation = updatedTeacher.VideoPresentation;
                    teacher.Certificates = updatedTeacher.Certificates;
                    _botDbContext.Teachers.Update(teacher);
                }

                await _botDbContext.SaveChangesAsync();
                return user.Id;
            }

            // Возвращаем -1, если пользователь не найден
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
                await _botDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<SignInTeacherObject> SignInTeacher(int telegramId)
        {
            var activityUpdate = await _botDbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == telegramId);

            if (activityUpdate != null)
            {
                activityUpdate.LastActivity = 2;
                _botDbContext.SaveChangesAsync();
            }


            var signIn = new SignInTeacherObject()
            {
                Teachers = await _botDbContext.Teachers.Include(u => u.User).FirstOrDefaultAsync(u => u.User.TelegramId == telegramId),
                UsefulLinks = await _botDbContext.TeacherUseFulUrls.ToListAsync()
            };

            return signIn;
        }

        public async Task<bool> SignOutTeacher(int telegramId)
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
