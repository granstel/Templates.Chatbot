using System.Collections.Generic;

namespace GranSteL.Chatbot.Models
{
    public class Response
    {
        public Response()
        {
            Buttons = new List<Button>();
        }

        public string ChatHash {get; set;}

        public string UserHash { get; set; }

        public string Text { get; set; }

        public string AlternativeText { get; set; }

        public bool Finished { get; set; }

        public ICollection<Button> Buttons { get; set; }
    }
}