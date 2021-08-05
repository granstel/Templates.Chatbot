using GranSteL.Chatbot.Services.Extensions;

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

        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public string ContentType { get; set; }
    }
}