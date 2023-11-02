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
        Task<ShowAllStudentsObject> ShowAllStudents(long telegramId);
        Task<ShowAllTeachersObject> ShowAllTeachers(long telegramId);
        Task<ShowAllOrdersObjectAdmin> AllOrders(long telegramId);
        Task<ShowAllReportsObject> ShowAllReports(ShowAllReportsInObject showAllReportsInObject);
        Task<ShowAllDisputesObject> AllDispute(long telegramId);
    }
}
