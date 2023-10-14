using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Common.ObjectRepository
{
    public class UserAccessQuery : IUserAccessQuery
    {
        private readonly IUserAccessRepository _userAccessRepository;

        public UserAccessQuery(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository;
        }

        public async Task<int> UserAccessId(int telegramId)
        {
            return await _userAccessRepository.UserAccessId(telegramId);
        }
    }
}
