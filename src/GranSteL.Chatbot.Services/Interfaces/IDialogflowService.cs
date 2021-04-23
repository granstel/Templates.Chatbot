using System.Threading.Tasks;
using GranSteL.Chatbot.Models;

namespace GranSteL.Chatbot.Services
{
    public interface IDialogflowService
    {
        Task<Dialog> GetResponseAsync(Request request);
    }
}