using RepititMe.Domain.Object;
using RepititMe.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Commands
{
    public interface IUserCommandService
    {
        Task<bool> UserSignUpStudent(UserSignUpObject userSignUpObject);
    }
}
