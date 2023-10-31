using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Common
{
    public interface IAdminRepository
    {
        Task<ShowAllStudentsObject> ShowAllStudents(int telegramId);
        Task<ShowAllTeachersObject> ShowAllTeachers(int telegramId);
        Task<bool> BlockingUser(BlockingUserObject blockingUserObject);
        Task<ShowAllOrdersObject> AllOrders(int telegramId);
        Task<ShowAllReportsObject> ShowAllReports(int telegramId, int orderId);
        Task<ShowAllDisputesObject> AllDispute(int telegramId); // Допилить Dispute
    }
}
