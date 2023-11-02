using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Admins.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BotDbContext _botDbContext;
        public AdminRepository(BotDbContext botDbContext)
        {
            _botDbContext = botDbContext;
        }

        public async Task<ShowAllDisputesObject> AllDispute(long telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllDisputesObject() { Status = false };
            }
            return new ShowAllDisputesObject() { Status = true, Disputes = await _botDbContext.Disputes.Include(t => t.Teacher).ThenInclude(u => u.User).Include(s => s.Student).ThenInclude(u => u.User).OrderByDescending(e => e.Id).ToListAsync() };
        }

        public async Task<ShowAllOrdersObjectAdmin> AllOrders(long telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllOrdersObjectAdmin() { Status = false };
            }
            return new ShowAllOrdersObjectAdmin() { Status = true, Orders = await _botDbContext.Orders.Include(t => t.Teacher).ThenInclude(u => u.User).Include(s => s.Student).ThenInclude(u => u.User).OrderByDescending(e => e.Id).ToListAsync() };
        }

        public async Task<bool> BlockingUser(BlockingUserObject blockingUserObject)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == blockingUserObject.TelegramIdAdmin && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return false;
            }

            var user = await _botDbContext.Users.Where(u => u.TelegramId == blockingUserObject.TelegramIdUser).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            else
            {
                user.Block = !user.Block;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> CloseDispute(CloseDisputeInObject closeDisputeObject)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == closeDisputeObject.TelegramIdAdmin && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return false;
            }

            var dispute = await _botDbContext.Disputes.Where(u => u.Id == closeDisputeObject.DisputeId).FirstOrDefaultAsync();
            if (dispute == null)
            {
                return false;
            }
            else
            {
                dispute.StatusClose = !dispute.StatusClose;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
        }

        public async Task<ShowAllReportsObject> ShowAllReports(ShowAllReportsInObject showAllReportsInObject)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == showAllReportsInObject.TelegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllReportsObject() { Status = false };
            }
            return new ShowAllReportsObject() { Status = true, Reports = await _botDbContext.Reports.Include(t => t.Order).Where(o => o.OrderId == showAllReportsInObject.OrderId).ToListAsync() };
        }

        public async Task<ShowAllStudentsObject> ShowAllStudents(long telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllStudentsObject() { Status = false };
            }
            return new ShowAllStudentsObject() { Status = true,  Students = await _botDbContext.Students
                .Include(u => u.User)
                .OrderByDescending(e => e.User.Id)
                .ToListAsync() };
        }

        public async Task<ShowAllTeachersObject> ShowAllTeachers(long telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllTeachersObject() { Status = false };
            }
            return new ShowAllTeachersObject() { Status = true,  Teachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .Include(u => u.Status)
                .Include(u => u.Science)
                .Include(u => u.LessonTarget)
                .Include(u => u.AgeCategory)
                .OrderByDescending(e => e.User.Id)
                .ToListAsync()};
        }
    }
}
