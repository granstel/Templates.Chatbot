using ApiAiSDK;
using Autofac;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Clients;
using GranSteL.Chatbot.Services.Configuration;
using RestSharp;

namespace GranSteL.Chatbot.Api.DependencyModules
{
    public class ExternalServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestClient>().As<IRestClient>();
            
            builder.RegisterType<QnaClient>().As<IQnaClient>();
            
            builder.Register(RegisterDialogflowClient).As<IApiAi>();

        }

        private IApiAi RegisterDialogflowClient(IComponentContext context)
        {
            var configuration = context.Resolve<DialogflowConfiguration>();

            var config = new AIConfiguration(configuration.Token, SupportedLanguage.Russian);

            var ai = new ApiAi(config);

            return ai;
        }
    }
}
