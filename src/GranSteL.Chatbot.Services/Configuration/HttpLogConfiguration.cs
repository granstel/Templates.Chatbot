namespace GranSteL.Chatbot.Services.Configuration
{
    public class HttpLogConfiguration
    {
        public HttpLogConfiguration()
        {
            ExcludeBodiesWithWords = new string[0];
            IncludeEndpoints = new string[0];
        }

        public bool Enabled { get; set; }

        public bool AddRequestIdHeader { get; set; }

        public string[] ExcludeBodiesWithWords { get; set; }

        public string[] IncludeEndpoints { get; set; }
    }
}
