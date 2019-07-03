using Autofac;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    /// <summary>
    /// Probably, registered at DependencyConfiguration of main project
    /// </summary>
    public class YandexDependencyModule : MessengerModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = GetConfigurationFromFile<YandexConfiguration>("appsettings.Yandex.json");

                return config;
            }).SingleInstance().AsSelf();

            builder.RegisterType<YandexController>().AsSelf();

            builder.RegisterType<YandexService>().As<IYandexService>();
            builder.RegisterType<YandexProfile>().AsSelf();
        }
    }
}
