using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Internal;

namespace GranSteL.Chatbot.Services
{
    public class ConversationService : IConversationService
    {
        public async Task<Response> GetResponseAsync(Request request)
        {
            //TODO: processing commands, invoking external services, and other cool asynchronous staff to generate response
            return await Task.Run(() => default(Response));
        }
    }
}
