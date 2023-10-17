using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities;

namespace RepititMe.Application.Services.Users.Queries
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository _userRepository;

        public UserQueryService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> UserAccessId(int telegramId)
        {
            return await _userRepository.UserAccessId(telegramId);
        }
    }
}
