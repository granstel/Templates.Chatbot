using GranSteL.Chatbot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GranSteL.Chatbot.Api.DependencyModules
{
    internal static class InternalServicesRegistration
    {
        internal static void AddInternalServices(this IServiceCollection services)
        {
            services.AddTransient<IConversationService, ConversationService>();
            services.AddScoped<IDialogflowService, DialogflowService>();
        }
    }
}
