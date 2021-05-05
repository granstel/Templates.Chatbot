using System.Collections.Generic;
using System.Linq;

namespace GranSteL.Chatbot.Models
{
    public class Dialog
    {
        public Dialog()
        {
            Parameters = new Dictionary<string, string>();
        }

        public IDictionary<string, string> Parameters { get; set; }

        public bool EndConversation { get; set; }

        public bool AllRequiredParamsPresent { get; set; }

        public string Response { get; set; }

        public string Action { get; set; }

        public Button[] Buttons { get; set; }

        public IEnumerable<string> GetParameters(string key)
        {
            return Parameters?.Where(p => string.Equals(p.Key, key)).Select(p => p.Value);
        }
    }
}
