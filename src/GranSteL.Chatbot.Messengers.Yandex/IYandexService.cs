using GranSteL.Chatbot.Services;
using Yandex.Dialogs.Models;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public interface IYandexService : IMessengerService<InputModel, OutputModel>
    {
    }
}