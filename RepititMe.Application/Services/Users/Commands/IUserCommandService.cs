using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepititMe.Domain.Object.Users;

namespace RepititMe.Application.Services.Users.Commands
{
    public interface IUserCommandService
    {
        Task<bool> UserSignUpStudent(UserSignUpStudentObject userSignUpStudent);
        Task<bool> UserSignUpTeacher(UserSignUpTeacherObject userSignUpTeacher);
    }
}
