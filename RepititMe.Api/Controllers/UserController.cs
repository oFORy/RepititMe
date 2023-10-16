using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Students.Commands;
using RepititMe.Application.Services.Students.Queries;
using RepititMe.Application.Services.Users.Commands;
using RepititMe.Application.Services.Users.Queries;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Object;
using System.ComponentModel;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class UserController : Controller
    {
        private readonly IUserQueryService _userAccessQuery;
        private readonly IUserCommandService _userCommandService;

        public UserController(IUserQueryService userAccessQuery, IUserCommandService userCommandService)
        {
            _userAccessQuery = userAccessQuery;
            _userCommandService = userCommandService;
        }

        [HttpGet("Api/User/Access")]
        public async Task<ActionResult<int>> UserAccessId(int telegramId)
        {
            return await _userAccessQuery.UserAccessId(telegramId);
        }


        [HttpPost("Api/User/SignUpStudent")]
        public async Task<ActionResult<bool>> UserSignUpStudent([FromBody] UserSignUpObject userSignUpObject)
        {
            return await _userCommandService.UserSignUpStudent(userSignUpObject);
        }
    }
}
