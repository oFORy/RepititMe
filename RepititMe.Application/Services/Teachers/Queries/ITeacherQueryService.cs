using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Teachers.Queries
{
    public interface ITeacherQueryService
    {
        Task<SignInTeacherObject> SignInTeacher(int telegramId);
        Task<bool> SignOutTeacher(int telegramId);
    }
}
