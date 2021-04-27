using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GranSteL.Chatbot.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Appeal
    {
        Default,
        NoOfficial,
        Official,
    }
}
