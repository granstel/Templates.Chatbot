using System.Collections.Generic;

namespace GranSteL.Chatbot.Models
{
    public class Payload
    {
        public Payload()
        {
            ButtonsForSource = new Dictionary<Source, ICollection<Button>>();
        }

        public IDictionary<Source, ICollection<Button>> ButtonsForSource { get; set; }
    }
}
