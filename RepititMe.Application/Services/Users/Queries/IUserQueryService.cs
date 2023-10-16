using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Queries
{
    public interface IUserQueryService
    {
        Task<int> UserAccessId(int telegramId);
    }
}
