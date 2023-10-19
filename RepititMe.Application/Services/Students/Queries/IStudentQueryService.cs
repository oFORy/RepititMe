using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.SearchCategory;
using RepititMe.Domain.Object.Students;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Queries
{
    public interface IStudentQueryService
    {
        Task<SignInStudentObject> SignInStudent(int telegramId);
        Task<List<BriefTeacherObject>> ShowTeachers(List<int> lastTeachers);
        Task<SearchCategoriesObject> SearchCategories();
    }
}
