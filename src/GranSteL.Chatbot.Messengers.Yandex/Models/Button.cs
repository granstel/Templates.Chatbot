using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Messengers.Yandex.Models
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Button
    {
        [JsonProperty]
        public string Title { get; set; }

        [JsonProperty]
        public Payload Payload { get; set; }

        [JsonProperty]
        public string Url { get; set; }

        [JsonProperty]
        public bool Hide { get; set; }
    }
}
