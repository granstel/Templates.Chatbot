namespace GranSteL.Chatbot.Services.Configuration
{
    public class HttpLogConfiguration
    {
        public bool Enabled { get; set; }

        public bool AddRequestIdHeader { get; set; }

        public string[] ExcludeBodiesWithWords { get; set; }
    }
}
