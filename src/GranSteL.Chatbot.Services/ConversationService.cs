using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Internal;

namespace GranSteL.Chatbot.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IDialogflowService _dialogflowService;

        public ConversationService(IDialogflowService dialogflowService)
        {
            _dialogflowService = dialogflowService;
        }

        public async Task<Response> GetResponseAsync(Request request)
        {
            //TODO: processing commands, invoking external services, and other cool asynchronous staff to generate response
            var dialog = await _dialogflowService.GetResponseAsync(request);

            var response = new Response { Text = dialog.Response, Finished = dialog.EndConversation };

            return response;
        }
    }
}
