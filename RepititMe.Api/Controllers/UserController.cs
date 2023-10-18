﻿using Microsoft.AspNetCore.Cors;
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


        [HttpPost("Api/User/SignUpTeacher")]
        public async Task<IActionResult> UserSignUpTeacher([FromForm] UserSignUpTeacherObject teacher)
        {
            try
            {
                var result = await _userCommandService.UserSignUpTeacher(teacher);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Показать полную информацию об учителе
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Api/User/FullTeacher")]
        public async Task<Teacher> FullTeacher(int userId)
        {
            return await _userQueryService.FullTeacher(userId);
        }
    }
}
