using GranSteL.Chatbot.Messengers.Yandex.Models;
using GranSteL.Chatbot.Services;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public interface IYandexService : IMessengerService<InputModel, OutputModel>
    {
    }
}