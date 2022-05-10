using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace GranSteL.Chatbot.Messengers.Chat2Desk.Tests
{
    [TestFixture]
    public class Chat2DeskClientIntegrationTests
    {
        private Chat2DeskConfiguration _configuration;
        private Chat2DeskClient _target;

        [SetUp]
        public void InitTest()
        {
            var loggerMock = Mock.Of<ILogger<Chat2DeskClient>>();

            var webClient = new RestClient { Proxy = new WebProxy("127.0.0.1", 8888) };

            _configuration = new Chat2DeskConfiguration
            {
                Url = "https://api.chat2desk.com/v1",
                Token = "%CHATBOT_CHAT2DESK_TOKEN%",
                IncomingToken = "%CHATBOT_CHAT2DESK_INCOMINGTOKEN%"
            };

            _target = new Chat2DeskClient(loggerMock, webClient, _configuration, new CustomJsonSerializer());
        }

        [Test]
        [Ignore("Integration")]
        public async Task SetWebhookAsync_Url_Success()
        {
            var url = $"http://localhost:19391/chat2desk/{_configuration.IncomingToken}";


            var result = await _target.SetWebhookAsync(url);


            Assert.True(result);
        }

        [Test]
        [Ignore("Integration")]
        public async Task SendTextMessageAsync_ChatId_Success()
        {
            var clientId = 0;
            var text = "Привет!";


            var result = await _target.SendTextMessageAsync(clientId, text);


            Assert.True(result);
        }
    }
}
