using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Teachers.Commands;
using RepititMe.Application.Services.Teachers.Queries;
using RepititMe.Domain.Object.Students;
using RepititMe.Domain.Object.Teachers;
using RepititMe.Domain.Object.Users;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class TeacherController : Controller
    {
        private readonly ITeacherCommandService _teacherCommandService;
        private readonly ITeacherQueryService _teacherQueryService;
        public TeacherController(ITeacherCommandService teacherCommandService, ITeacherQueryService teacherQueryService)
        {
            _teacherCommandService = teacherCommandService;
            _teacherQueryService = teacherQueryService;
        }


        /// <summary>
        /// Вход для учителя
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Teacher/SignIn")]
        public async Task<SignInTeacherObject> SignInTeacher(int telegramId)
        {
            return await _teacherQueryService.SignInTeacher(telegramId);
        }

        /// <summary>
        /// Выход из аккаунта учителя
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Teacher/SignOut")]
        public async Task<bool> SignOutTeacher(int telegramId)
        {
            return await _teacherQueryService.SignOutTeacher(telegramId);
        }


        /// <summary>
        /// Изменение профиля учителя
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost("Api/Teacher/ChangeProfile")]
        public async Task<IActionResult> ChangeProfile([FromForm] ChangeProfileTeacherObject teacher)
        {
            try
            {
                var result = await _teacherCommandService.ChangeProfile(teacher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Меняет видимость анкеты на противоположное значение
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Teacher/ChangeVisability")]
        public async Task<bool> ChangeVisability(int telegramId)
        {
            return await _teacherCommandService.ChangeVisability(telegramId);
        }
    }
}
