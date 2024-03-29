﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Users;

namespace RepititMe.Application.Services.Users.Queries
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository _userRepository;

        public UserQueryService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Teacher> FullTeacher(long telegramId)
        {
            return await _userRepository.FullTeacher(telegramId);
        }

        public async Task<Dictionary<string, int>> UserAccessId(long telegramId)
        {
            return await _userRepository.UserAccessId(telegramId);
        }
    }
}
