using Autofac;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Clients;
using GranSteL.Chatbot.Services.Configuration;
using Grpc.Auth;
using RestSharp;

namespace GranSteL.Chatbot.Api.DependencyModules
{
    public class ExternalServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RestClient>().As<IRestClient>();
            
            builder.RegisterType<QnaClient>().As<IQnaClient>();
            
            builder.Register(RegisterDialogflowClient).As<SessionsClient>();

        }

        private SessionsClient RegisterDialogflowClient(IComponentContext context)
        {
            var configuration = context.Resolve<DialogflowConfiguration>();

            var credential = GoogleCredential.FromFile(configuration.JsonPath).CreateScoped(SessionsClient.DefaultScopes);

            var channel = new Grpc.Core.Channel(SessionsClient.DefaultEndpoint.ToString(), credential.ToChannelCredentials());

            var client = SessionsClient.Create(channel);

            return client;
        }
    }
}
