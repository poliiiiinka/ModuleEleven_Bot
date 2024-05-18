using SF_PJ03_TelegramBot.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_PJ03_TelegramBot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            // возвращаем сессию по ключу, если она существует
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            // создаём и возвращаем новую, если такой не было
            var newSession = new Session() { Operation = "math" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
