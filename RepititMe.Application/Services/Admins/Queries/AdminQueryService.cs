using RepititMe.Application.Services.Admins.Common;
using RepititMe.Domain.Entities.Users;
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

        public async Task<List<Student>> ShowAllStudents(int telegramId)
        {
            return await _adminRepository.ShowAllStudents(telegramId);
        }

        public async Task<List<Teacher>> ShowAllTeachers(int telegramId)
        {
            return await _adminRepository.ShowAllTeachers(telegramId);
        }
    }
}
