﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Students.Commands;
using RepititMe.Application.Services.Students.Queries;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object.SearchCategory;
using RepititMe.Domain.Object.Students;
using RepititMe.Domain.Object.Teachers;
using System.ComponentModel;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class StudentController : Controller
    {
        private readonly IStudentQueryService _studentQueryService;
        private readonly IStudentCommandService _studentCommandService;

        public StudentController(IStudentQueryService studentQueryService, IStudentCommandService studentCommandService)
        {
            _studentCommandService = studentCommandService;
            _studentQueryService = studentQueryService;
        }

        /// <summary>
        /// Вход для ученика, а так же первую 5 учителей для скрола
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Student/SignIn")]
        public async Task<SignInStudentObject> SignInStudent(long telegramId)
        {
            return await _studentQueryService.SignInStudent(telegramId);
        }

        /// <summary>
        /// Выход из аккаунта ученика
        /// </summary>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        [HttpGet("Api/Student/SignOut")]
        public async Task<bool> SignOutStudent(long telegramId)
        {
            return await _studentCommandService.SignOutStudent(telegramId);
        }

        /// <summary>
        /// Показать все категории для поиска
        /// </summary>
        /// <returns></returns>
        [HttpGet("Api/Student/SearchCategories")]
        public async Task<SearchCategoriesObject> SearchCategories()
        {
            return await _studentQueryService.SearchCategories();
        }

        /// <summary>
        /// Показывает 5 учителей для скрола, за исключением UserId учителей из списка
        /// </summary>
        /// <param name="lastTeachers"></param>
        /// <returns></returns>
        [HttpPost("Api/Student/ShowTeachers")]
        public async Task<List<BriefTeacherObject>> ShowTeachers([FromBody] ShowTeachersFilterObject showTeachersFilterObject)
        {
            return await _studentQueryService.ShowTeachers(showTeachersFilterObject);
        }

        /// <summary>
        /// Показать 5 лучших учителей с импользованием категорий
        /// </summary>
        /// <param name="searchCategoriesResultObject"></param>
        /// <returns></returns>
        [HttpPost("Api/Student/ResultSearch")]
        public async Task<List<BriefTeacherObject>> ResultSearchCategories([FromBody] SearchCategoriesResultObject searchCategoriesResultObject)
        {
            return await _studentCommandService.ResultSearchCategories(searchCategoriesResultObject);
        }

        /// <summary>
        /// Редактировать профиль ученика
        /// </summary>
        /// <param name="telegramId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPut("Api/Student/ChangeProfile")]
        public async Task<bool> ChangeProfile(long telegramId, string name)
        {
            return await _studentCommandService.ChangeProfile(telegramId, name);
        }
    }
}
