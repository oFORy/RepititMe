using RepititMe.Application.Services.Students.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Commands
{
    public class StudentCommandService : IStudentCommandService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentCommandService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> ChangeProfile(int telegramId, string name)
        {
            return await _studentRepository.ChangeProfile(telegramId, name);
        }

        public async Task<bool> SignOutStudent(int telegramId)
        {
            return await _studentRepository.SignOutStudent(telegramId);
        }
    }
}
