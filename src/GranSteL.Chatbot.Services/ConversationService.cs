using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Internal;

namespace GranSteL.Chatbot.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IDialogflowService _dialogflowService;

        //TODO: uncomment when you fill in the Dialogflow settings, or remove it
        public ConversationService(/*IDialogflowService dialogflowService*/)
        {
            //_dialogflowService = dialogflowService;
        }

        public async Task<Response> GetResponseAsync(Request request)
        {
            //TODO: processing commands, invoking external services, and other cool asynchronous staff to generate response

            //TODO: uncomment when you fill in the Dialogflow settings, or remove it
            //var dialog = await _dialogflowService.GetResponseAsync(request);
            //var response = new Response { Text = dialog.Response, Finished = dialog.EndConversation };

            var response = new Response { Text = "Success test!" };

            return response;
        }
    }
}
