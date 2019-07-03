using System;

namespace GranSteL.Chatbot.Services
{
    public interface IDialogflowService
    {
        string GetGreeting(string name);

        string GetCity(string requestMessageText);

        DateTime? GetDate(string requestMessageText);
    }
}