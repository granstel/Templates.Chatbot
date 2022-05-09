using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sber.SmartApp.Models;

namespace GranSteL.Chatbot.Messengers.Sber
{
    public class SberController : MessengerController<Request, Response>
    {
        public SberController(ISberService sberService, SberConfiguration configuration) : base(sberService,
            configuration)
        {
            SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
