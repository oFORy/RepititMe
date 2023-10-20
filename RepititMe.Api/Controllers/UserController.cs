using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Students.Commands;
using RepititMe.Application.Services.Students.Queries;
using RepititMe.Application.Services.Users.Commands;
using RepititMe.Application.Services.Users.Queries;
using RepititMe.Domain.Entities;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.Users;
using System.ComponentModel;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class UserController : Controller
    {
        private readonly IUserQueryService _userQueryService;
        private readonly IUserCommandService _userCommandService;

        public UserController(IUserQueryService userQueryService, IUserCommandService userCommandService)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
        }

        /// <summary>
        /// Вернет последнюю активность или ноль (вышел из аккаунта/первый раз)
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/User/Access")]
        public async Task<ActionResult<Dictionary<string, int>>> UserAccessId(int telegramId)
        {
            return await _userQueryService.UserAccessId(telegramId);
        }

        /// <summary>
        /// Регистрация для ученика
        /// </summary>
        /// <param name="userSignUpObject"></param>
        /// <returns></returns>
        [HttpPost("Api/User/SignUpStudent")]
        public async Task<ActionResult<bool>> UserSignUpStudent([FromBody] UserSignUpStudentObject userSignUpObject)
        {
            return await _userCommandService.UserSignUpStudent(userSignUpObject);
        }

        /// <summary>
        /// Регистрация для учителя
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost("Api/User/SignUpTeacher")]
        public async Task<bool> UserSignUpTeacher([FromBody] UserSignUpTeacherObject teacher)
        {
            return await _userCommandService.UserSignUpTeacher(teacher);
        }

        /// <summary>
        /// Показать полную информацию об учителе
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Api/User/FullTeacher")]
        public async Task<Teacher> FullTeacher(int telegramId)
        {
            return await _userQueryService.FullTeacher(telegramId);
        }

        /// <summary>
        /// Вернет файл по пути
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost("Api/User/FullTeacher/Files")]
        public IActionResult GetFile([FromBody] string fileName)
        {
            if (!System.IO.File.Exists(fileName))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(fileName);
            return File(fileBytes, "application/octet-stream", Path.GetFileName(fileName));
        }
    }
}
