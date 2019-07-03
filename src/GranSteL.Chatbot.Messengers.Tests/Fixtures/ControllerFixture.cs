using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Configuration;

namespace GranSteL.Chatbot.Messengers.Tests.Fixtures
{
    public class ControllerFixture : MessengerController<InputFixture, OutputFixture>
    {
        public ControllerFixture(IMessengerService<InputFixture, OutputFixture> messengerService, MessengerConfiguration configuration) : base(messengerService, configuration)
        {
        }
    }
}
