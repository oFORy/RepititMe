using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Common.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly BotDbContext _botDbContext;
        public UserAccessRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public async Task<int> UserAccessId(int telegramId)
        {
            var isStudent = await _botDbContext.Students.AnyAsync(s => s.TelegramId == telegramId);
            var isTeacher = await _botDbContext.Teachers.AnyAsync(t => t.TelegramId == telegramId);

            if (isStudent && isTeacher)
            {
                return 3;
            }
            else if (isStudent)
            {
                return 1;
            }
            else if (isTeacher)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }
}
