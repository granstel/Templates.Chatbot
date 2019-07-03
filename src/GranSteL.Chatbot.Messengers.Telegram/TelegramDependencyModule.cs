using System;
using Autofac;
using Telegram.Bot;

namespace GranSteL.Chatbot.Messengers.Telegram
{
    /// <summary>
    /// Probably, registered at DependencyConfiguration of main project
    /// </summary>
    public class TelegramDependencyModule : MessengerModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = GetConfigurationFromFile<TelegramConfiguration>("appsettings.Telegram.json");

                return config;
            }).SingleInstance().AsSelf();

            builder.RegisterType<TelegramController>().AsSelf();

            builder.Register(RegisterTelegramClient).As<ITelegramBotClient>();

            builder.RegisterType<TelegramService>().As<ITelegramService>();
        }

        private TelegramBotClient RegisterTelegramClient(IComponentContext context)
        {
            var configuration = context.Resolve<TelegramConfiguration>();

            var telegramClient = new TelegramBotClient(configuration.Token)
            {
                Timeout = TimeSpan.FromMinutes(3)
            };

            return telegramClient;
        }
    }
}
