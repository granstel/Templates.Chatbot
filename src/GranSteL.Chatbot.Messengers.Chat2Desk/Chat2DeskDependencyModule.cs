using Autofac;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    /// <summary>
    /// Probably, registered at DependencyConfiguration of main project
    /// </summary>
    public class Chat2DeskDependencyModule : MessengerModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = GetConfigurationFromFile<Chat2DeskConfiguration>("appsettings.Chat2Desk.json");

                return config;
            }).SingleInstance().AsSelf();

            builder.RegisterType<Chat2DeskController>().AsSelf();

            builder.RegisterType<Chat2DeskService>().As<IChat2DeskService>();
            builder.RegisterType<Chat2DeskClient>().As<IChat2DeskClient>();
        }
    }
}
