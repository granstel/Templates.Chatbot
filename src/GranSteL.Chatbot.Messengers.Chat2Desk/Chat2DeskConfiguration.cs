using GranSteL.Chatbot.Services.Configuration;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class Chat2DeskConfiguration : MessengerConfiguration
    {
        private string _url;
        public virtual string Url
        {
            get => _url;
            set => _url = ExpandVariable(value);
        }
    }
}
