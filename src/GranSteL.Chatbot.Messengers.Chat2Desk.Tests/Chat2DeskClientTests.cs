using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using GranSteL.Chatbot.Services.Extensions;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace GranSteL.Chatbot.Messengers.Chat2Desk.Tests
{
    [TestFixture]
    public class Chat2DeskClientTests
    {
        private MockRepository _mockRepository;

        private Mock<IRestClient> _webClient;
        private Mock<Chat2DeskConfiguration> _configuration;

        private Chat2DeskClient _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _webClient = _mockRepository.Create<IRestClient>();
            _configuration = _mockRepository.Create<Chat2DeskConfiguration>();
            _configuration.SetupGet(c => c.Url).Returns("http://test.test");

            _target = new Chat2DeskClient(_webClient.Object, _configuration.Object, new CustomJsonSerializer());

            _fixture = new Fixture();
        }

        [Test]
        public void ConfigurationUrl_Used()
        {
            _configuration.VerifyGet(c => c.Url);
        }

        #region SetWebhookAsync

        [Test]
        public async Task SetWebhookAsync_Resource_Correct()
        {
            var request = default(IRestRequest);

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.SetWebhookAsync(It.IsAny<string>());


            _mockRepository.VerifyAll();

            var expected = "/companies/web_hook";
            Assert.AreEqual(expected, request.Resource);
            Assert.AreEqual(Method.POST, request.Method);
        }

        [Test]
        public async Task SetWebHookAsync_ConfigurationToken_Used()
        {
            var request = default(IRestRequest);

            var token = _fixture.Create<string>();

            _configuration.SetupGet(c => c.Token).Returns(token);

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.SetWebhookAsync(It.IsAny<string>());


            _mockRepository.VerifyAll();

            var header = request.Parameters.FirstOrDefault(p => string.Equals("Authorization", p.Name))?.Value;
            Assert.AreEqual(token, header);
        }

        [Test]
        public async Task SetWebHookAsync_Url_Used()
        {
            var request = default(IRestRequest);

            var url = _fixture.Create<string>();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.SetWebhookAsync(url);


            _mockRepository.VerifyAll();

            var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value.Serialize();

            Assert.NotNull(body);
            Assert.True(body.Contains(url));

            var expectedActions = "inbox";

            Assert.True(body.Contains(expectedActions));
        }

        [Test]
        public async Task SetWebHookAsync_OkStatusCode_Success()
        {
            var response = _fixture.Build<RestResponse>()
                               .With(r => r.StatusCode, HttpStatusCode.OK)
                               .OmitAutoProperties()
                               .Create();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var result = await _target.SetWebhookAsync(It.IsAny<string>());


            _mockRepository.VerifyAll();

            Assert.True(result);
        }

        [Test]
        public async Task SetWebHookAsync_Exception_Success()
        {
            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();


            var result = await _target.SetWebhookAsync(It.IsAny<string>());


            _mockRepository.VerifyAll();

            Assert.False(result);
        }

        #endregion SetWebhookAsync

        #region DeleteWebHookAsync

        [Test]
        public async Task DeleteWebHookAsync_Resource_Correct()
        {
            var request = default(IRestRequest);

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.DeleteWebHookAsync();


            _mockRepository.VerifyAll();

            var expected = "/companies/web_hook";
            Assert.AreEqual(expected, request.Resource);
            Assert.AreEqual(Method.POST, request.Method);
        }

        [Test]
        public async Task DeleteWebHookAsync_ConfigurationToken_Used()
        {
            var request = default(IRestRequest);

            var token = _fixture.Create<string>();

            _configuration.SetupGet(c => c.Token).Returns(token);

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.DeleteWebHookAsync();


            _mockRepository.VerifyAll();

            var header = request.Parameters.FirstOrDefault(p => string.Equals("Authorization", p.Name))?.Value;
            Assert.AreEqual(token, header);
        }

        [Test]
        public async Task DeleteWebHookAsync_Url_Used()
        {
            var request = default(IRestRequest);

            var url = _fixture.Create<string>();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.DeleteWebHookAsync();


            _mockRepository.VerifyAll();

            var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value.Serialize();

            Assert.NotNull(body);
            Assert.False(body.Contains(url));

            var expected = "\"events\":null";

            Assert.True(body.Contains(expected));
        }

        [Test]
        public async Task DeleteWebHookAsync_OkStatusCode_Success()
        {
            var response = _fixture.Build<RestResponse>()
                               .With(r => r.StatusCode, HttpStatusCode.OK)
                               .OmitAutoProperties()
                               .Create();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var result = await _target.DeleteWebHookAsync();


            _mockRepository.VerifyAll();

            Assert.True(result);
        }

        [Test]
        public async Task DeleteWebHookAsync_Exception_Success()
        {
            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();


            var result = await _target.DeleteWebHookAsync();


            _mockRepository.VerifyAll();

            Assert.False(result);
        }

        #endregion DeleteWebHookAsync

        #region SendTextMessageAsync

        [Test]
        public async Task SendTextMessageAsync_Resource_Correct()
        {
            var request = default(IRestRequest);

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);

            await _target.SendTextMessageAsync(It.IsAny<int>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            var expected = "/messages";
            Assert.AreEqual(expected, request.Resource);
            Assert.AreEqual(Method.POST, request.Method);
        }

        [Test]
        public async Task SendTextMessageAsync_ConfigurationToken_Used()
        {
            var request = default(IRestRequest);

            var token = _fixture.Create<string>();

            _configuration.SetupGet(c => c.Token).Returns(token);

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.SendTextMessageAsync(It.IsAny<int>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            var header = request.Parameters.FirstOrDefault(p => string.Equals("Authorization", p.Name))?.Value;
            Assert.AreEqual(token, header);
        }

        [Test]
        public async Task SendTextMessageAsync_Parameters_Used()
        {
            var request = default(IRestRequest);

            var chatId = _fixture.Create<int>();
            var text = _fixture.Create<string>();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Callback((IRestRequest r, CancellationToken t) => request = r);


            await _target.SendTextMessageAsync(chatId, text);


            _mockRepository.VerifyAll();

            var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value.Serialize();

            Assert.NotNull(body);
            Assert.True(body.Contains(chatId.ToString()));
            Assert.True(body.Contains(text));
        }

        [Test]
        public async Task SendTextMessageAsync_SuccessStatus_Success()
        {
            var expected = "suCcEsS";

            var sendInformation = _fixture.Build<SendInformation>()
                .With(i => i.Status, expected)
                .OmitAutoProperties()
                .Create();

            var response = _fixture.Build<RestResponse>()
                               .With(r => r.StatusCode, HttpStatusCode.OK)
                               .With(r => r.Content, sendInformation.Serialize())
                               .OmitAutoProperties()
                               .Create();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var result = await _target.SendTextMessageAsync(It.IsAny<int>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            Assert.True(result);
        }

        [Test]
        public async Task SendTextMessageAsync_NullResponseContent_Success()
        {
            var response = _fixture.Build<RestResponse>()
                .With(r => r.StatusCode, HttpStatusCode.OK)
                .Without(r => r.Content)
                .OmitAutoProperties()
                .Create();

            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);


            var result = await _target.SendTextMessageAsync(It.IsAny<int>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            Assert.False(result);
        }

        [Test]
        public async Task SendTextMessageAsync_NullResponse_Success()
        {
            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);


            var result = await _target.SendTextMessageAsync(It.IsAny<int>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            Assert.False(result);
        }

        [Test]
        public async Task SendTextMessageAsync_Exception_Success()
        {
            _webClient.Setup(c =>
                c.ExecuteAsync(It.IsAny<IRestRequest>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();


            var result = await _target.SendTextMessageAsync(It.IsAny<int>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            Assert.False(result);
        }

        #endregion SendTextMessageAsync
    }
}
