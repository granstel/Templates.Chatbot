using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Messengers.Tests.Fixtures
{
    public class ControllerFixture : MessengerController<InputFixture, OutputFixture>
    {
        public ControllerFixture(ILogger<ControllerFixture> log, IMessengerService<InputFixture, OutputFixture> messengerService, MessengerConfiguration configuration)
            : base(log, messengerService, configuration)
        {
        }
    }
}
