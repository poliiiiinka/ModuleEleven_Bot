using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using SF_PJ03_TelegramBot.Controllers;
using SF_PJ03_TelegramBot.Services;
using SF_PJ03_TelegramBot.Configuration;

namespace SF_PJ03_TelegramBot
{
    public class Program
    {
        static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // объект, отвечающий за постоянный ЖЦ приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // задаём конфигурацию
                .UseConsoleLifetime() // позволяет поддерживать приложение активным в консоли
                .Build(); // собираем

            Console.WriteLine("Service is running");
            await host.RunAsync(); // запускаем сервис
            Console.WriteLine("Service has been stopped");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // инициализация конфигурации
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings);

            // подключаем хранилище сессий
            services.AddSingleton<IStorage, MemoryStorage>();

            // подключаем контроллеры
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<InlineKeyboardController>();
            services.AddTransient<TextMessageController>();

            // регистируем объект TelegramBotClient с токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            // регистируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }

        /// <summary>
        /// метод, инициализирующий конфигурацию
        /// </summary>
        /// <returns></returns>
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "7026700492:AAGwDfY1d1WuctM5afOYqzNJXSpxwYAyFR0"
            };
        }
    }
}
