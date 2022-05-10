using System.Threading.Tasks;
using AutoFixture;
using GranSteL.Chatbot.Messengers.Tests.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Telegram.Bot.Types;

namespace GranSteL.Chatbot.Messengers.Telegram.Tests
{
    [TestFixture]
    public class TelegramControllerTests : ControllerTests<TelegramController>
    {
        private Mock<ITelegramService> _telegramService;
        private Mock<TelegramConfiguration> _configuration;

        [SetUp]
        public void InitTest()
        {
            InitTestBase();

            var loggerMock = Mock.Of<ILogger<TelegramController>>();
            _telegramService = MockRepository.Create<ITelegramService>();
            _configuration = MockRepository.Create<TelegramConfiguration>();

            Target = new TelegramController(loggerMock, _telegramService.Object, _configuration.Object);
        }

        [Test]
        public async Task TestTelegramApi_TestApiAsync_Response()
        {
            var expected = true;

            _telegramService.Setup(s => s.TestApiAsync()).ReturnsAsync(expected);


            var result = await Target.TestTelegramApiAsync();


            MockRepository.VerifyAll();

            var value = (result as JsonResult)?.Value;
            Assert.AreEqual(expected, value);
        }


        [Test]
        public async Task GetMeAsync_GetMeAsync_Response()
        {
            var user = Fixture.Create<User>();

            _telegramService.Setup(s => s.GetMeAsync()).ReturnsAsync(user);


            var result = await Target.GetMeAsync();


            MockRepository.VerifyAll();

            var value = (result as JsonResult)?.Value;
            Assert.AreEqual(user, value);
        }
    }
}
