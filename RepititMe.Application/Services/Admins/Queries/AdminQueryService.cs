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

        public async Task<ShowAllDisputesObject> AllDispute(int telegramId)
        {
            return await _adminRepository.AllDispute(telegramId);
        }

        public async Task<ShowAllOrdersObjectAdmin> AllOrders(int telegramId)
        {
            return await _adminRepository.AllOrders(telegramId);
        }

        public async Task<ShowAllReportsObject> ShowAllReports(int telegramId, int orderId)
        {
            return await _adminRepository.ShowAllReports(telegramId, orderId);
        }

        public async Task<ShowAllStudentsObject> ShowAllStudents(int telegramId)
        {
            return await _adminRepository.ShowAllStudents(telegramId);
        }

        public async Task<ShowAllTeachersObject> ShowAllTeachers(int telegramId)
        {
            return await _adminRepository.ShowAllTeachers(telegramId);
        }
    }
}
