﻿using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Common
{
    public interface IUserRepository
    {
        Task<int> UserAccessId(int telegramId);
        Task<bool> UserSignUpStudent(UserSignUpObject userSignUpObject);
    }
}