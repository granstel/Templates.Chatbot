using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Internal;

namespace GranSteL.Chatbot.Services
{
    public interface IConversationService
    {
        Task<Response> GetResponseAsync(Request request);
    }
}