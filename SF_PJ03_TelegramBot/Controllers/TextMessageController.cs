using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using SF_PJ03_TelegramBot.Configuration;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using SF_PJ03_TelegramBot.Services;

namespace SF_PJ03_TelegramBot.Controllers
{
    /// <summary>
    /// контроллер для обработки текстовых сообщений
    /// </summary>
    public class TextMessageController
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient botClient, IStorage memoryStorage)
        {
            _botClient = botClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken cancellationToken)
        {
            switch (message.Text)
            {
                case "/start":
                    // объект, представляющий кнопки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Посчитать сумму чисел", "math"),
                        InlineKeyboardButton.WithCallbackData($"Посчитать символы в тексте", "symbol")
                    });
                    // передаём кнопки вместе с сообщением
                    await _botClient.SendTextMessageAsync(message.Chat.Id,
                        $"<b> Бот может посчитать сумму чисел или количество символов в тексте </b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Напишите числа через пробел или отправьте текстовое сообщение{Environment.NewLine}",
                        cancellationToken: cancellationToken, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;
                default:
                    switch (_memoryStorage.GetSession(message.From.Id).Operation)
                    {
                        case "math":
                            string[] numbers = message.Text.Split(' ');
                            int sum = 0;
                            foreach (string s in numbers)
                            {
                                if (int.TryParse(s, out int value))
                                    sum += value;
                            }
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"Сумма чисел: {sum}", cancellationToken: cancellationToken);
                            break;
                        case "symbol":
                            await _botClient.SendTextMessageAsync(message.Chat.Id, $"Количество символов в тексте: {message.Text.Length}", cancellationToken: cancellationToken);
                            break;
                    }
                    break;
            }
        }
    }
}
