using System.Threading.Tasks;

namespace GranSteL.Chatbot.Services
{
    public interface IQnaService
    {
        Task<string> GetAnswerAsync(string question);
    }
}