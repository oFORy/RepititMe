using Telegram.Bot;
using Telegram.Bot.Types.InlineQueryResults;

namespace RepititMe.Application.bot.Services
{
    public interface ITelegramService
    {
        public Task SendActionAsync(string message, string id);
    }

    public class TelegramService : ITelegramService
    {

        private readonly ITelegramBotClient _botClient;

        public TelegramService(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SendActionAsync(string message, string id)
        {

            await _botClient.SendTextMessageAsync(id, message);

            // Query Id
        }

    }
}
