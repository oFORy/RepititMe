using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Queries
{
    public interface IAdminQueryService
    {
        Task<ShowAllStudentsObject> ShowAllStudents(int telegramId);
        Task<ShowAllTeachersObject> ShowAllTeachers(int telegramId);
        Task<ShowAllOrdersObject> AllOrders(int telegramId);
        Task<ShowAllReportsObject> ShowAllReports(int telegramId, int orderId);
        Task<ShowAllDisputesObject> AllDispute(int telegramId);
    }
}
