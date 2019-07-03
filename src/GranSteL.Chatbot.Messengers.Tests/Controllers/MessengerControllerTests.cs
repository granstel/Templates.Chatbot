using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using GranSteL.Chatbot.Messengers.Tests.Fixtures;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace GranSteL.Chatbot.Messengers.Tests.Controllers
{
    [TestFixture]
    public class MessengerControllerTests : ControllerTests<ControllerFixture>
    {
        private Mock<IMessengerService<InputFixture, OutputFixture>> _messengerService;
        private Mock<MessengerConfiguration> _configuration;

        private readonly string _tokenParameter = "token";

        [SetUp]
        public void InitTest()
        {
            InitTestBase();

            _messengerService = MockRepository.Create<IMessengerService<InputFixture, OutputFixture>>();
            _configuration = MockRepository.Create<MessengerConfiguration>();

            Target = new ControllerFixture(_messengerService.Object, _configuration.Object);
        }

        #region OnActionExecuting

        [Test]
        public void OnActionExecuting_InvalidToken_NotFound()
        {
            var token = Fixture.Create<string>();

            _configuration.SetupGet(c => c.IncomingToken).Returns(Fixture.Create<string>());

            var actionArguments = new Dictionary<string, object> { { _tokenParameter, token } };

            var context = GetActionContext(actionArguments);


            Target.OnActionExecuting(context);


            Assert.True(context.Result is NotFoundResult);
        }

        [Test]
        public void OnActionExecuting_NullConfigurationToken_NotFound()
        {
            var token = Fixture.Create<string>();

            _configuration.SetupGet(c => c.IncomingToken).Returns(() => null);

            var actionArguments = new Dictionary<string, object> { { _tokenParameter, token } };

            var context = GetActionContext(actionArguments);


            Target.OnActionExecuting(context);


            Assert.True(context.Result is NotFoundResult);
        }

        [Test]
        public void OnActionExecuting_ValidToken_NullResult()
        {
            var token = Fixture.Create<string>();

            _configuration.SetupGet(c => c.IncomingToken).Returns(token);

            var actionArguments = new Dictionary<string, object> { { _tokenParameter, token } };

            var context = GetActionContext(actionArguments);


            Target.OnActionExecuting(context);


            Assert.Null(context.Result);
        }

        [Test]
        public void OnActionExecuting_NullTokens_NullResult()
        {
            string token = null;

            _configuration.SetupGet(c => c.IncomingToken).Returns(token);

            var actionArguments = new Dictionary<string, object> { { _tokenParameter, token } };

            var context = GetActionContext(actionArguments);


            Target.OnActionExecuting(context);


            Assert.Null(context.Result);
        }

        #endregion OnActionExecuting

        [Test]
        public void Get_GetWebHookUrl_Success()
        {
            SetControllerContext();

            var expected = GetExpectedWebHookUrl();


            var result = Target.GetInfo();


            Assert.True(result.Contains(expected));
        }

        [Test]
        public async Task Post_ProcessIncomingAsync_Response()
        {
            var input = Fixture.Build<InputFixture>().Create();

            var token = Fixture.Create<string>();

            var expected = Fixture.Create<OutputFixture>();

            _messengerService.Setup(s => s.ProcessIncomingAsync(input)).ReturnsAsync(expected);


            var result = await Target.WebHook(input, token);


            MockRepository.VerifyAll();
            var value = (result as JsonResult)?.Value;
            Assert.AreEqual(expected, value);
        }

        [Test]
        public async Task CreateWebHook_SetWebhookAsync_Response()
        {
            SetControllerContext();

            var url = GetExpectedWebHookUrl();

            var expected = true;

            _messengerService.Setup(s => s.SetWebhookAsync(url)).ReturnsAsync(expected);


            var result = await Target.CreateWebHook(null);


            MockRepository.VerifyAll();

            var value = (result as JsonResult)?.Value;
            Assert.AreEqual(expected, value);
        }

        [Test]
        public async Task DeleteWebHook_DeleteWebHookAsync_Response()
        {
            var expected = true;

            _messengerService.Setup(s => s.DeleteWebhookAsync()).ReturnsAsync(expected);

            var result = await Target.DeleteWebHook(null);


            MockRepository.VerifyAll();

            var value = (result as JsonResult)?.Value;
            Assert.AreEqual(expected, value);
        }
    }
}
