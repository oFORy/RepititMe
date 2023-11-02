using RepititMe.Application.Services.Admins.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Queries
{
    public class AdminQueryService : IAdminQueryService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminQueryService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<ShowAllDisputesObject> AllDispute(long telegramId)
        {
            return await _adminRepository.AllDispute(telegramId);
        }

        public async Task<ShowAllOrdersObjectAdmin> AllOrders(long telegramId)
        {
            return await _adminRepository.AllOrders(telegramId);
        }

        public async Task<ShowAllReportsObject> ShowAllReports(ShowAllReportsInObject showAllReportsInObject)
        {
            return await _adminRepository.ShowAllReports(showAllReportsInObject);
        }

        public async Task<ShowAllStudentsObject> ShowAllStudents(long telegramId)
        {
            return await _adminRepository.ShowAllStudents(telegramId);
        }

        public async Task<ShowAllTeachersObject> ShowAllTeachers(long telegramId)
        {
            return await _adminRepository.ShowAllTeachers(telegramId);
        }
    }
}
