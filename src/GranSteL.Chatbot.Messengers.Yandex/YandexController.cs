using Microsoft.Extensions.Logging;
using Yandex.Dialogs.Models;
using Yandex.Dialogs.Models.Input;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public class YandexController : MessengerController<InputModel, OutputModel>
    {
        public YandexController(ILogger<YandexController> log, IYandexService yandexService, YandexConfiguration configuration)
            : base(log, yandexService, configuration)
        {
        }
    }
}
