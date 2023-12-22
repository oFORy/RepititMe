using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Notifications.Common
{
    public interface INotificationRepository
    {
        public Task<(List<Order> timeAccept, List<Order> timeFirstLesson)> NotificationsDataAsync();
        public Task NotificationsConfirm(int orderId, int typeId);
    }
}
