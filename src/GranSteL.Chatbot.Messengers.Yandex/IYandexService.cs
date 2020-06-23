using GranSteL.Chatbot.Services;
using Yandex.Dialogs.Models;
using Yandex.Dialogs.Models.Input;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public interface IYandexService : IMessengerService<InputModel, OutputModel>
    {
    }
}