using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Admins.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class AdminRepository : IAdminRepository
    {
        private readonly BotDbContext _botDbContext;
        public AdminRepository(BotDbContext botDbContext)
        {
            _botDbContext = botDbContext;
        }
    }
}
