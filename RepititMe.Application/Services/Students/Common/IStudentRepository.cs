using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Common
{
    public interface IStudentRepository
    {
        Task<SignInStudentObject> SignInStudent(int telegramId);
        Task<bool> ChangeProfile(int telegramId, string image = null, string name = null);
        Task<List<Teacher>> ShowTeachers(int lastNumber);
    }
}
