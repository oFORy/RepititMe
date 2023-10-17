using RepititMe.Application.Services.Students.Common;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Queries
{
    public class StudentQueryService : IStudentQueryService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentQueryService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<SearchCategoriesObject> SearchCategories()
        {
             return await _studentRepository.SearchCategories();
        }

        public async Task<List<Teacher>> ShowTeachers(List<int> lastTeachers)
        {
            return await _studentRepository.ShowTeachers(lastTeachers);
        }

        public async Task<SignInStudentObject> SignInStudent(int telegramId)
        {
            return await _studentRepository.SignInStudent(telegramId);
        }
    }
}
