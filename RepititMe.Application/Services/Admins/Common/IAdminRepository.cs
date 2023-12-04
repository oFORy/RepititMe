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
        Task<ShowAllStudentsObject> ShowAllStudents(long telegramId);
        Task<ShowAllTeachersObject> ShowAllTeachers(long telegramId);
        Task<bool> BlockingUser(BlockingUserObject blockingUserObject);
        Task<ShowAllOrdersObjectAdmin> AllOrders(long telegramId);
        Task<ShowAllReportsObject> ShowAllReports(ShowAllReportsInObject showAllReportsInObject);
        Task<ShowAllDisputesObject> AllDispute(long telegramId);
        Task<bool> CloseDispute(CloseDisputeInObject closeDisputeObject);
        Task<bool> CheckAdmin(long telegramId);
    }
}
