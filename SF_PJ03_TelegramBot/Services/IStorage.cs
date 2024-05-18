using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF_PJ03_TelegramBot.Models;

namespace SF_PJ03_TelegramBot.Services
{
    public interface IStorage
    {
        /// <summary>
        /// получение сессии пользователя по идентификатору
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Session GetSession(long chatId);
    }
}
