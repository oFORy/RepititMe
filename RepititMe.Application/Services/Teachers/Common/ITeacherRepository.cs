using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Teachers.Common
{
    public interface ITeacherRepository
    {
        Task<SignInTeacherObject> SignInTeacher(long telegramId);
        Task<bool> SignOutTeacher(long telegramId);
        Task<int> ChangeProfile(Teacher teacher, long telegramId);
        Task<bool> UpdateTeacherDataFolder(UpdateTeacherDataFolderObject updateTeacherDataFolderObject);
        Task<bool> ChangeVisability(long telegramId);
    }
}
