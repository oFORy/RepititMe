using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using RepititMe.Api.bot.Services;
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
        //private readonly ITelegramService _telegramService;

        public UserController(IUserQueryService userQueryService, IUserCommandService userCommandService/*, ITelegramService telegramService*/)
        {
            _userQueryService = userQueryService;
            _userCommandService = userCommandService;
            //_telegramService = telegramService;
        }

        /// <summary>
        /// Вернет последнюю активность или ноль (вышел из аккаунта/первый раз)
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/User/Access")]
        public async Task<ActionResult<Dictionary<string, int>>> UserAccessId(long telegramId/*, long testId*/)
        {

            //await _telegramService.SendActionAsync(testId.ToString(), "Привет");
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
        public async Task<Teacher> FullTeacher(long telegramId)
        {
            return await _userQueryService.FullTeacher(telegramId);
        }

        /// <summary>
        /// Вернет видео файл
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Api/User/FullTeacher/Files/video")]
        public IActionResult GetFile_video([FromBody] FileNameModel model)
        {
            var filePath = Path.Combine(model.FilePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var file = new PhysicalFileResult(filePath, "video/mp4")
            {
                EnableRangeProcessing = true
            };

            return file;
        }

        /// <summary>
        /// Вернет изображение
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Api/User/FullTeacher/Files/image")]
        public IActionResult GetFile_image([FromBody] FileNameModel model)
        {
            var filePath = Path.Combine(model.FilePath);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileContentResult = new PhysicalFileResult(filePath, "application/octet-stream")
            {
                FileDownloadName = model.FilePath
            };
            return fileContentResult;
        }

        public class FileNameModel
        {
            public string FilePath { get; set; }
        }
    }
}
