using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Common
{
    public interface IUserRepository
    {
        Task<Dictionary<string, int>> UserAccessId(int telegramId);
        Task<bool> UserSignUpStudent(UserSignUpStudentObject userSignUpObject);
        Task<bool> UserSignUpTeacher(UserSignUpTeacherObject userSignUpTeacherObject);
        Task<Teacher> FullTeacher(int userId);
    }
}
