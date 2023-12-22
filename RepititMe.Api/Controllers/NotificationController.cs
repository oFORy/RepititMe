using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.bot.Services;
using RepititMe.Application.Services.Notifications.Common;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class NotificationController : Controller
    {
        private readonly ITelegramService _telegramService;
        private readonly INotificationRepository _notificationRepository;
        public NotificationController(ITelegramService telegramService, INotificationRepository notificationRepository)
        {
            _telegramService = telegramService;
            _notificationRepository = notificationRepository;
        }


        [HttpGet("Api/Notifications/Data")]
        public async Task<IActionResult> NotificationsDataAsync()
        {
            var data = await _notificationRepository.NotificationsDataAsync();
            return new JsonResult(new { Successfully = true, TimeAccept = data.timeAccept, TimeFirstLesson = data.timeFirstLesson });
        }


        [HttpPost("Api/Notifications/Send")]
        public async Task NotificationsSendAsync([FromBody] NotificationsRequestParam pararm)
        {
            await _telegramService.SendNotifications(pararm.TeacherTelegramId, pararm.StudentTelegramId, pararm.TypeId);
            await _notificationRepository.NotificationsConfirm(pararm.OrderId, pararm.TypeId);
        }
    }

    public class NotificationsRequestParam
    {
        public int OrderId { get; set; }
        public long TeacherTelegramId { get; set; }
        public long StudentTelegramId { get; set; }
        public int TypeId { get; set; }
    }
}
