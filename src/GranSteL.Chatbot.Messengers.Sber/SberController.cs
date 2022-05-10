using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sber.SmartApp.Models;

namespace GranSteL.Chatbot.Messengers.Sber
{
    public class SberController : MessengerController<Request, Response>
    {
        public SberController(ILogger<SberController> log, ISberService sberService, SberConfiguration configuration)
            : base(log, sberService, configuration)
        {
            SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
