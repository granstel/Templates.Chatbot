namespace GranSteL.Chatbot.Services.Configuration
{
    public class DialogflowConfiguration : Configuration
    {
        private string _token;
        public string Token
        {
            get => _token;
            set => _token = ExpandVariable(value);
        }
    }
}
