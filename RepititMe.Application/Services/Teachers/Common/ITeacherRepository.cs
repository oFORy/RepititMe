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
        Task<int> ChangeProfile(Teacher teacher, int telegramId);
        Task<bool> UpdateTeacherDataFolder(UpdateTeacherDataFolderObject updateTeacherDataFolderObject);
        Task<bool> ChangeVisability(int telegramId);
    }
}
