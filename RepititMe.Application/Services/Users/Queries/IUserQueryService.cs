using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Queries
{
    public interface IUserQueryService
    {
        Task<Dictionary<string, int>> UserAccessId(int telegramId);
        Task<Teacher> FullTeacher(int userId);
    }
}
