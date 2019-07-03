namespace GranSteL.Chatbot.Models.Internal
{
    public class Request
    {
        public Source Source {get; set;}

        public string ChatHash {get; set;}

        public string UserHash { get; set; }

        public string RequestText { get; set; }
    }
}