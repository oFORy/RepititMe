using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Common
{
    public interface IAdminRepository
    {
        Task<List<Student>> ShowAllStudents();
        Task<List<Teacher>> ShowAllTeachers();
    }
}
