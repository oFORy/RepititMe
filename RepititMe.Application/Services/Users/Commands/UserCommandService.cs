using Microsoft.AspNetCore.Http;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Teachers;
using RepititMe.Domain.Object.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Users.Commands
{
    public class UserCommandService : IUserCommandService
    {
        private readonly IUserRepository _userRepository;

        public UserCommandService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UserSignUpStudent(UserSignUpStudentObject userSignUpStudent)
        {
            return await _userRepository.UserSignUpStudent(userSignUpStudent);
        }

        public async Task<bool> UserSignUpTeacher(UserSignUpTeacherObject userSignUpTeacherObject)
        {
            return await _userRepository.UserSignUpTeacher(userSignUpTeacherObject);
        }
    }
}
