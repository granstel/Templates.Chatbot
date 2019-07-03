using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Messengers.Chat2Desk.Models
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Message
    {
        [JsonProperty]
        public int MessageId { get; set; }

        [JsonProperty]
        public string Text { get; set; }

        [JsonProperty]
        public object Coordinates { get; set; }

        [JsonProperty]
        public string Transport { get; set; }

        [JsonProperty]
        public string Type { get; set; }

        [JsonProperty]
        public bool Read { get; set; }

        [JsonProperty]
        public string Created { get; set; }

        [JsonProperty]
        public long ClientId { get; set; }

        [JsonProperty]
        public string VbBusId { get; set; }

        [JsonProperty]
        public string RecipientStatus { get; set; }
    }
}
