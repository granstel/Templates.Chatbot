using GranSteL.Chatbot.Services.Extensions;
using RestSharp.Serializers;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class CustomJsonSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            ContentType = "application/json";

            var result = obj.Serialize();

            return result;
        }

        public string ContentType { get; set; }
    }
}