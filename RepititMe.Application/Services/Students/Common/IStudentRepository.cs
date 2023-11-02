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
        Task<SignInStudentObject> SignInStudent(long telegramId);
        Task<bool> ChangeProfile(long telegramId, string name);
        Task<bool> SignOutStudent(long telegramId);
        Task<List<BriefTeacherObject>> ShowTeachers(ShowTeachersFilterObject showTeachersFilterObject);
        Task<SearchCategoriesObject> SearchCategories();
        Task<List<BriefTeacherObject>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject);
        
    }
}
