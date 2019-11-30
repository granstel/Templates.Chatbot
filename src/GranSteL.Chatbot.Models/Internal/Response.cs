namespace GranSteL.Chatbot.Models.Internal
{
    public class Response
    {
        public string ChatHash {get; set;}

        public string UserHash { get; set; }

        public string Text { get; set; }

        public string AlternativeText { get; set; }

        public bool Finished { get; set; }
    }
}