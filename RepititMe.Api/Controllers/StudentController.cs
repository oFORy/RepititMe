using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Students.Commands;
using RepititMe.Application.Services.Students.Queries;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System.ComponentModel;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentQueryService _studentQueryService;
        private readonly IStudentCommandService _studentCommandService;

        public StudentController(IStudentQueryService studentQueryService, IStudentCommandService studentCommandService)
        {
            _studentCommandService = studentCommandService;
            _studentQueryService = studentQueryService;
        }


        [HttpGet("Api/Student/SignIn")]
        public async Task<SignInStudentObject> SignInStudent(int telegramId)
        {
            return await _studentQueryService.SignInStudent(telegramId);
        }


        [HttpGet("Api/Student/SignOut")]
        public async Task<bool> SignOutStudent(int telegramId)
        {
            return await _studentCommandService.SignOutStudent(telegramId);
        }

        [HttpGet("Api/Student/SearchCategories")]
        public async Task<SearchCategoriesObject> SearchCategories()
        {
            return await _studentQueryService.SearchCategories();
        }

        [HttpPost("Api/Student/ShowTeachers")]
        public async Task<List<BriefTeacher>> ShowTeachers([FromBody] List<int> lastTeachers)
        {
            return await _studentQueryService.ShowTeachers(lastTeachers);
        }

        [HttpPost("Api/Student/ResultSearch")]
        public async Task<List<BriefTeacher>> ResultSearchCategories([FromBody] SearchCategoriesResultObject searchCategoriesResultObject)
        {
            return await _studentCommandService.ResultSearchCategories(searchCategoriesResultObject);
        }

        [HttpPut("Api/Student/ChangeProfile")]
        public async Task<bool> ChangeProfile(int telegramId, string name)
        {
            return await _studentCommandService.ChangeProfile(telegramId, name);
        }
    }
}
