using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using SF_PJ03_TelegramBot.Services;
using SF_PJ03_TelegramBot.Configuration;

namespace SF_PJ03_TelegramBot.Controllers
{
    /// <summary>
    /// контроллер для обработки нажатий на кнопки
    /// </summary>
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _botClient;

        public InlineKeyboardController(IStorage memoryStorage, ITelegramBotClient botClient)
        {
            _memoryStorage = memoryStorage;
            _botClient = botClient;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken cancellationToken)
        {
            if (callbackQuery?.Data == null)
                return;

            // обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).Operation = callbackQuery.Data;

            // генерируем информационное сообщение
            string operationText = callbackQuery.Data switch
            {
                "math" => "Сумма чисел",
                "symbol" => "Количество символов",
                _ => String.Empty
            };

            // отправляем в ответ уведомление о выборе
            await _botClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b> Выбранная операция - {operationText} </b>", cancellationToken: cancellationToken, parseMode: ParseMode.Html);
        }
    }
}
