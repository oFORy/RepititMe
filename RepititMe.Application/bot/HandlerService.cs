using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.Logging;
using RepititMe.Application.Services.Admins.Common;

namespace RepititMe.Application.bot
{
    public class HandlerService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ILogger<HandlerService> _logger;
        private readonly IAdminRepository _adminRepository;


        public HandlerService(ITelegramBotClient botClient, ILogger<HandlerService> logger, IAdminRepository adminRepository)
        {
            _botClient = botClient;
            _logger = logger;
            _adminRepository = adminRepository;
        }

        public async Task EchoAsync(Update update)
        {
            try
            {
                _logger.LogInformation(update.Type.ToString());
                switch (update.Type)
                {
                    case UpdateType.Message:
                        await BotOnMessageReceived(update.Message!);
                        break;
                    default:
                        await UnknownUpdateHandlerAsync(update);
                        break;
                };
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(exception);
            }
        }

        private async Task BotOnMessageReceived(Message message)
        {
            _logger.LogInformation($"Receive message type: {message.Type}");
            if (message.Type != MessageType.Text)
                return;

            Message sentMessage = new Message();

            switch (message.Text!.Split(' ')[0])
            {
                case "/start":
                    sentMessage = await SendFirstMessage(message);
                    break;
                case "/admin":
                    if (await _adminRepository.CheckAdmin(message.From.Id))
                        sentMessage = await SendAdminMessage(message);
                    break;
                default:
                    break;
            }

            _logger.LogInformation($"The message was sent with id: {sentMessage.MessageId}");
        }

        private async Task<Message> SendFirstMessage(Message message)
        {
            string usage = "Привет" + (message?.From?.FirstName != null ? ", " + message?.From?.FirstName + "! " : "! ") +
                           "RepetitMe – сервис для подбора онлайн-репетиторов по всей России. Находите преподавателей для повышения успеваемости, подготовки к экзаменам и олимпиадам в удобном современном формате через приложение в Telegram по всем предметам. Быстро, безопасно и удобно!Наша база репетиторов постоянно растет, чтобы Вы смогли найти идеального преподавателя под свои задачи.\r\nЕсли Вы - преподаватель, то заполните анкету в приложение и получайте дополнительных учеников.";

            return await _botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                         text: usage,
                                                         replyMarkup: new ReplyKeyboardRemove());
        }

        private async Task<Message> SendAdminMessage(Message message)
        {

            var replyMarkup = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithWebApp("Администратор", new WebAppInfo
                    {
                        Url = "https://webapp.repetitmeweb.ru/admin"
                    })
                }
            });

            return await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Вы входите в админ-панель приложения",
                replyMarkup: replyMarkup,
                replyToMessageId: message.MessageId
            );
        }



        private Task UnknownUpdateHandlerAsync(Update update)
        {
            _logger.LogInformation("Unknown update type: {updateType}", update.Type);
            return Task.CompletedTask;
        }


        public Task HandleErrorAsync(Exception exception)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _logger.LogInformation("HandleError: {ErrorMessage} | StackTrace: {StackTrace}", ErrorMessage, exception.StackTrace);
            return Task.CompletedTask;
        }
    }
}
