using RepititMe.Application.Services.Admins.Common;
using RepititMe.Domain.Object.Admins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Admins.Commands
{
    public class AdminCommandService : IAdminCommandService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminCommandService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<bool> BlockingUser(BlockingUserObject blockingUserObject)
        {
            return await _adminRepository.BlockingUser(blockingUserObject);
        }

        public async Task<bool> CloseDispute(CloseDisputeInObject closeDisputeObject)
        {
            return await _adminRepository.CloseDispute(closeDisputeObject);
        }
    }
}
