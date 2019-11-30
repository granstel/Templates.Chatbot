using System;
using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Internal;

namespace GranSteL.Chatbot.Services
{
    public interface IDialogflowService
    {
        Task<Dialog> GetResponseAsync(Request request);
    }
}