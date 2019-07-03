namespace GranSteL.Chatbot.Services.Configuration
{
    public abstract class MessengerConfiguration : Configuration
    {
        private string _incomingToken;
        public virtual string IncomingToken
        {
            get => _incomingToken;
            set => _incomingToken = ExpandVariable(value);
        }

        private string _token;
        public virtual string Token
        {
            get => _token;
            set => _token = ExpandVariable(value);
        }
    }
}
