using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Student.Commands
{
    public interface IStudentCommandService
    {
        Task<bool> ChangeProfile(int telegramId, string image = null, string name = null);
    }
}
