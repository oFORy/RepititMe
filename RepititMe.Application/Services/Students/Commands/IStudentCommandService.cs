using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Students.Commands
{
    public interface IStudentCommandService
    {
        Task<bool> ChangeProfile(int telegramId, string name);
        Task<bool> SignOutStudent(int telegramId);
        Task<List<BriefTeacher>> ResultSearchCategories(SearchCategoriesResultObject searchCategoriesResultObject);
    }
}
