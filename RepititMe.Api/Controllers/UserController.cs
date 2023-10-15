using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Common.ObjectRepository;
using RepititMe.Application.Services.Student.Commands;
using RepititMe.Application.Services.Student.Queries;
using System.ComponentModel;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class UserController : Controller
    {
        private readonly IUserAccessQuery _userAccessQuery;

        public UserController(IUserAccessQuery userAccessQuery)
        {
            _userAccessQuery = userAccessQuery;
        }

        [HttpGet("Api/User")]
        public async Task<ActionResult<int>> UserAccessId(int telegramId)
        {
            return await _userAccessQuery.UserAccessId(telegramId);
        }
    }
}
