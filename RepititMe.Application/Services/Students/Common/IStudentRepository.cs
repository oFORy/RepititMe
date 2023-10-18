using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.SearchCategory;
using RepititMe.Domain.Object.Students;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Common
{
    public interface IStudentRepository
    {
        Task<SignInStudentObject> SignInStudent(int telegramId);
        Task<bool> ChangeProfile(int telegramId, string name);
        Task<bool> SignOutStudent(int telegramId);
        Task<List<BriefTeacher>> ShowTeachers(List<int> lastTeachers);
        Task<SearchCategoriesObject> SearchCategories();
        Task<List<BriefTeacher>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject);
        
    }
}
