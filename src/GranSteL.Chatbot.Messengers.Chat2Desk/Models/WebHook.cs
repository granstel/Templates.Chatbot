using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Messengers.Chat2Desk.Models
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class WebHook
    {
        [JsonProperty]
        public string Url { get; set; }

        [JsonProperty]
        public string[] Events { get; set; }
    }
}
