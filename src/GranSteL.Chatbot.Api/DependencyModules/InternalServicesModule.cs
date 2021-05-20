using Autofac;
using GranSteL.Chatbot.Services;

namespace GranSteL.Chatbot.Api.DependencyModules
{
    public class InternalServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConversationService>().As<IConversationService>();
            builder.RegisterType<DialogflowService>().As<IDialogflowService>();
        }
    }
}
