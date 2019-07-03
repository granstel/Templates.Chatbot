using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Qna;

namespace GranSteL.Chatbot.Services
{
    public interface IQnaClient
    {
        Task<Response> GetAnswerAsync(string knowledgeBase, string question);
    }
}