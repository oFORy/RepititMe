using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.SearchCategory;
using RepititMe.Domain.Object.Teachers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Commands
{
    public interface IStudentCommandService
    {
        Task<bool> ChangeProfile(long telegramId, string name);
        Task<bool> SignOutStudent(long telegramId);
        Task<List<BriefTeacherObject>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject);
    }
}
