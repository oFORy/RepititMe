using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Data;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
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

        public async Task<int> UserAccessId(int telegramId)
        {
            var isUser = await _botDbContext.Users.FirstOrDefaultAsync(s => s.TelegramId == telegramId);
            if (isUser == null)
            {
                return 0;
            }
            else
            {
                return isUser.LastActivity;
            }
        }

        public async Task<bool> UserSignUpStudent(UserSignUpObject userSignUpObject)
        {
            var newUser = new User()
            {
                TelegramId = userSignUpObject.TelegramId,
                Name = userSignUpObject.Name,
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

            var check = await _botDbContext.Users.FirstOrDefaultAsync(c => c.TelegramId == userSignUpObject.TelegramId);

            if (check == null)
                return false;
            else
                return true;
        }
    }
}
