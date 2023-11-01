﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<ShowAllDisputesObject> AllDispute(int telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllDisputesObject() { Status = false };
            }
            return new ShowAllDisputesObject() { Status = true, Disputes = await _botDbContext.Disputes.OrderByDescending(e => e.Id).ToListAsync() };
        }

        public async Task<ShowAllOrdersObject> AllOrders(int telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllOrdersObject() { Status = false };
            }
            return new ShowAllOrdersObject() { Status = true, Orders = await _botDbContext.Orders.OrderByDescending(e => e.Id).ToListAsync() };
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

        public async Task<ShowAllReportsObject> ShowAllReports(int telegramId, int orderId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllReportsObject() { Status = false };
            }
            return new ShowAllReportsObject() { Status = true, Reports = await _botDbContext.Reports.Where(o => o.OrderId == orderId).ToListAsync() };
        }

        public async Task<ShowAllStudentsObject> ShowAllStudents(int telegramId)
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

        public async Task<ShowAllTeachersObject> ShowAllTeachers(int telegramId)
        {
            var check = await _botDbContext.Users.Where(u => u.TelegramId == telegramId && u.Admin).FirstOrDefaultAsync();
            if (check == null)
            {
                return new ShowAllTeachersObject() { Status = false };
            }
            return new ShowAllTeachersObject() { Teachers = await _botDbContext.Teachers
                .Include(u => u.User)
                .OrderByDescending(e => e.User.Id)
                .ToListAsync(), Status = true };
        }
    }
}