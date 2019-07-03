using GranSteL.Chatbot.Models.Yandex;
using GranSteL.Chatbot.Services;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public interface IYandexService : IMessengerService<InputModel, OutputModel>
    {
    }
}