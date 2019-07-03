using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Models.Yandex
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class InputModel
    {
        [JsonProperty]
        public MetaModel Meta { get; set; }

        [JsonProperty]
        public Request Request { get; set; }

        [JsonProperty]
        public InputSession Session { get; set; }

        [JsonProperty]
        public string Version { get; set; }
    }
}
