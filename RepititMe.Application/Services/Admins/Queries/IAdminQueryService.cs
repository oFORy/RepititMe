using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Queries
{
    public interface IAdminQueryService
    {
        Task<List<Student>> ShowAllStudents(int telegramId);
        Task<List<Teacher>> ShowAllTeachers(int telegramId);
    }
}
