using System.ServiceModel;

namespace GranSteL.Chatbot.Services.Configuration
{
    public class WcfServiceConfiguration : Configuration
    {
        public BasicHttpBinding Binding { get; set; }

        private string _uri;
        public string Uri
        {
            get => _uri;
            set => _uri = ExpandVariable(value);
        }

        private EndpointAddress _endpointAddress;

        public EndpointAddress EndpointAddress
        {
            get
            {
                if (_endpointAddress == null)
                {
                    _endpointAddress = new EndpointAddress(Uri);
                }

                return _endpointAddress;
            }
        }
    }
}
