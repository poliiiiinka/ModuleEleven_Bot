using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using SF_PJ03_TelegramBot.Configuration;

namespace SF_PJ03_TelegramBot.Controllers
{
    /// <summary>
    /// контроллер для обработки сообщений неподдерживаемого формата
    /// </summary>
    public class DefaultMessageController
    {
        private readonly ITelegramBotClient _botClient;

        public DefaultMessageController(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task Handle(Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _botClient.SendTextMessageAsync(message.Chat.Id, "Получено сообщение неподдерживаемого формата", cancellationToken: cancellationToken);
        }
    }
}
