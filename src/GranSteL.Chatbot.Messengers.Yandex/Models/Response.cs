using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Messengers.Yandex.Models
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Response
    {
        [JsonProperty]
        public string Text { get; set; }

        [JsonProperty]
        public string Tts { get; set; }

        [JsonProperty]
        public Button[] Buttons { get; set; }

        [JsonProperty]
        public bool EndSession { get; set; }
    }
}
