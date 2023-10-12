using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Student.Commands;
using RepititMe.Application.Services.Student.Queries;

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

    }
}
