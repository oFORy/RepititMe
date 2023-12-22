using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Notifications.Common;
using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly BotDbContext _botDbContext;
        public NotificationRepository(BotDbContext botDbContext)
        {
            _botDbContext = botDbContext;
        }

        public async Task NotificationsConfirm(int orderId, int typeId)
        {
            var data = await _botDbContext.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
            if (data != null)
            {
                if (typeId == 1)
                {
                    data.NotificationTimeAccept = true;
                    await _botDbContext.SaveChangesAsync();
                }
                else if (typeId == 2)
                {
                    data.NotificationTimeFirstLesson = true;
                    await _botDbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<(List<Order> timeAccept, List<Order> timeFirstLesson)> NotificationsDataAsync()
        {
            var timeAccept = await _botDbContext.Orders
                .Include(t => t.Teacher)
                    .ThenInclude(u => u.User)
                .Include(s => s.Student)
                    .ThenInclude(u => u.User)
                .Where(o => !o.RefusedTeacher && !o.RefusedStudent && o.DateTimeAccept != null)
                .Where(time => !time.NotificationTimeAccept)
                .ToListAsync();

            var timeFirstLesson = await _botDbContext.Orders
                .Include(t => t.Teacher)
                    .ThenInclude(u => u.User)
                .Include(s => s.Student)
                    .ThenInclude(u => u.User)
                .Where(o => !o.RefusedTeacher && !o.RefusedStudent && o.DateTimeFirstLesson != null)
                .Where(time => !time.NotificationTimeFirstLesson && time.NotificationTimeAccept)
                .ToListAsync();

            return (timeAccept, timeFirstLesson);
        }


    }
}
