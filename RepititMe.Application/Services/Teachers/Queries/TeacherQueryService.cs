using RepititMe.Application.Services.Teachers.Common;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Teachers.Queries
{
    public class TeacherQueryService : ITeacherQueryService
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherQueryService(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<SignInTeacherObject> SignInTeacher(int telegramId)
        {
            return await _teacherRepository.SignInTeacher(telegramId);
        }

        public async Task<bool> SignOutTeacher(int telegramId)
        {
            return await _teacherRepository.SignOutTeacher(telegramId);
        }
    }
}
