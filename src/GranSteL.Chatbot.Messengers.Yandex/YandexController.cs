using GranSteL.Chatbot.Models.Yandex;
using Microsoft.AspNetCore.Mvc;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    [Produces("application/json")]
    [Route("/Yandex")]
    public class YandexController : MessengerController<InputModel, OutputModel>
    {
        public YandexController(IYandexService yandexService, YandexConfiguration configuration) : base(yandexService, configuration)
        {
        }
    }
}
