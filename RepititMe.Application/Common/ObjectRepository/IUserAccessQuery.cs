﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Common.ObjectRepository
{
    public interface IUserAccessQuery
    {
        Task<int> UserAccessId(int telegramId);
    }
}