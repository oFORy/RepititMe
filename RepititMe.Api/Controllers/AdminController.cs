using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Admins.Commands;
using RepititMe.Application.Services.Admins.Queries;
using RepititMe.Domain.Entities.Users;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Admins;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class AdminController : Controller
    {
        private readonly IAdminQueryService _adminQueryService;
        private readonly IAdminCommandService _adminCommandService;
        public AdminController(IAdminQueryService adminQueryService, IAdminCommandService adminCommandService)
        {
            _adminCommandService = adminCommandService;
            _adminQueryService = adminQueryService;
        }

        [HttpPost("Api/Admin/ShowAll/Students")]
        public async Task<ShowAllStudentsObject> ShowAllStudents([FromBody] int telegramId)
        {
            return await _adminQueryService.ShowAllStudents(telegramId);
        }


        [HttpPost("Api/Admin/ShowAll/Teachers")]
        public async Task<ShowAllTeachersObject> ShowAllTeachers([FromBody] int telegramId)
        {
            return await _adminQueryService.ShowAllTeachers(telegramId);
        }

        [HttpPost("Api/Admin/Blocking")]
        public async Task<bool> BlockingUser([FromBody] BlockingUserObject blockingUserObject)
        {
            return await _adminCommandService.BlockingUser(blockingUserObject);
        }

        [HttpPost("Api/Admin/ShowAll/Orders")]
        public async Task<ShowAllOrdersObjectAdmin> AllOrders(int telegramId)
        {
            return await _adminQueryService.AllOrders(telegramId);
        }

        [HttpPost("Api/Admin/ShowAll/Reports")]
        public async Task<ShowAllReportsObject> ShowAllReports(int telegramId, int orderId)
        {
            return await _adminQueryService.ShowAllReports(telegramId, orderId);
        }

        [HttpPost("Api/Admin/ShowAll/Dispute")]
        public async Task<ShowAllDisputesObject> AllDispute(int telegramId)
        {
            return await _adminQueryService.AllDispute(telegramId);
        }
    }
}
