using System.Collections.Generic;

namespace GranSteL.Chatbot.Models
{
    public class Payload
    {
        public Payload()
        {
            // string - messanger name
            ButtonsForMessangers = new Dictionary<string, ICollection<Button>>();
        }

        public IDictionary<string, ICollection<Button>> ButtonsForMessangers { get; set; }
    }
}
