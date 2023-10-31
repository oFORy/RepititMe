using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Commands
{
    public interface IAdminCommandService
    {
        Task<bool> BlockingUser(BlockingUserObject blockingUserObject);
    }
}
