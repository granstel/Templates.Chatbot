using GranSteL.Chatbot.Services;
using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Input;

namespace GranSteL.Chatbot.Messengers.Marusia
{
    public interface IMarusiaService : IMessengerService<InputModel, OutputModel>
    {
    }
}