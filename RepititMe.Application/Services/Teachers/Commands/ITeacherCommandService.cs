using RepititMe.Domain.Object.Teachers;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Teachers.Commands
{
    public interface ITeacherCommandService
    {
        Task<bool> ChangeProfile(ChangeProfileTeacherObject changeProfileTeacherObject);
        Task<bool> ChangeVisability(int telegramId);
    }
}
