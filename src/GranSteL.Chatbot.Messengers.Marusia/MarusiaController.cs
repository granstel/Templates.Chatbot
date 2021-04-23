using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GranSteL.Chatbot.Messengers.Marusia
{
    [Produces("application/json")]
    public class MarusiaController : MessengerController<InputModel, OutputModel>
    {
        public MarusiaController(IMarusiaService marusiaService, MarusiaConfiguration configuration) : base(marusiaService, configuration)
        {
            SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
