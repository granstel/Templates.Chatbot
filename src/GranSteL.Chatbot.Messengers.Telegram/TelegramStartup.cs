using System;
using GranSteL.Chatbot.Messengers.Telegram;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

[assembly: HostingStartup(typeof(TelegramStartup))]
namespace GranSteL.Chatbot.Messengers.Telegram
{
    public class TelegramStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddConfiguration<TelegramConfiguration>("appsettings.Telegram.json");

                services.AddTransient<ITelegramService, TelegramService>();
                services.AddTransient<ITelegramBotClient>(RegisterTelegramClient);
            });
        }

        private TelegramBotClient RegisterTelegramClient(IServiceProvider provider)
        {
            var configuration = provider.GetService<TelegramConfiguration>();

            var telegramClient = new TelegramBotClient(configuration.Token)
            {
                Timeout = TimeSpan.FromMinutes(3)
            };

            return telegramClient;
        }
    }
}
