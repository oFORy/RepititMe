using RepititMe.Application.Services.Notifications.Common;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InlineQueryResults;

namespace RepititMe.Application.bot.Services
{
    public interface ITelegramService
    {
        public Task SendActionAsync(string message, string id);
        public Task SendNotifications(long teacherTelegramId, long studentTelegramId, int typeId);
    }

    public class TelegramService : ITelegramService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ITelegramBotClient _botClient;

        public TelegramService(ITelegramBotClient botClient, INotificationRepository notificationRepository)
        {
            _botClient = botClient;
            _notificationRepository = notificationRepository;
        }

        public async Task SendActionAsync(string message, string id)
        {

            await _botClient.SendTextMessageAsync(id, message);

            // Query Id
        }

        public async Task SendNotifications(long teacherTelegramId, long studentTelegramId, int typeId)
        {
            if (typeId == 1)
            {
                string message = "Здравствуйте, пройдите опрос в приложении о договоренности, спасибо!";
                await _botClient.SendTextMessageAsync(teacherTelegramId, message);
                await _botClient.SendTextMessageAsync(studentTelegramId, message);
            }
            else if (typeId == 2)
            {
                string message = "Здравствуйте, пройдите опрос в приложении о первом занятии, спасибо!";
                await _botClient.SendTextMessageAsync(teacherTelegramId, message);
                await _botClient.SendTextMessageAsync(studentTelegramId, message);
            }
        }
    }
}
