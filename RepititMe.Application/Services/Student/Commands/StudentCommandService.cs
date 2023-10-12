using RepititMe.Application.Services.Student.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Student.Commands
{
    public class StudentCommandService : IStudentCommandService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentCommandService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> ChangeProfile(int telegramId, string image = null, string name = null)
        {
            return await _studentRepository.ChangeProfile(telegramId, image, name);
        }
    }
}
