using RepititMe.Domain.Entities;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Student.Queries
{
    public interface IStudentQueryService
    {
        Task<SignInStudentObject> SignInStudent(int telegramId);
        Task<List<Teacher>> ShowTeachers(int lastNumber);
    }
}
