using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Input;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GranSteL.Chatbot.Messengers.Marusia
{
    public class MarusiaController : MessengerController<InputModel, OutputModel>
    {
        public MarusiaController(ILogger<MarusiaController> log, IMarusiaService marusiaService, MarusiaConfiguration configuration)
            : base(log, marusiaService, configuration)
        {
            SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
