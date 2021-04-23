using Autofac;

namespace FillInTheTextBot.Messengers.Marusia
{
    /// <summary>
    /// Probably, registered at DependencyConfiguration of main project
    /// </summary>
    public class MarusiaDependencyModule : MessengerModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var config = GetConfigurationFromFile<MarusiaConfiguration>("appsettings.Marusia.json");

                return config;
            }).SingleInstance().AsSelf();

            builder.RegisterType<MarusiaController>().AsSelf();

            builder.RegisterType<MarusiaService>().As<IMarusiaService>();
            builder.RegisterType<MarusiaProfile>().AsSelf();
        }
    }
}
