using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GranSteL.Chatbot.Messengers.Yandex.Models
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class Payload 
	{
	}
}