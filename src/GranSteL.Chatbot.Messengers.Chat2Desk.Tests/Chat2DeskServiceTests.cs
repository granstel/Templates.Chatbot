using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using GranSteL.Chatbot.Models.Internal;
using GranSteL.Chatbot.Services;
using Moq;
using NUnit.Framework;

namespace GranSteL.Chatbot.Messengers.Chat2Desk.Tests
{
    [TestFixture]
    public class Chat2DeskServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<IChat2DeskClient> _client;
        private Mock<IConversationService> _conversationService;
        private Mock<IMapper> _mapper;

        private Chat2DeskService _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _client = _mockRepository.Create<IChat2DeskClient>();
            _conversationService = _mockRepository.Create<IConversationService>();
            _mapper = _mockRepository.Create<IMapper>();

            _target = new Chat2DeskService(_client.Object, _conversationService.Object, _mapper.Object);

            _fixture = new Fixture();
        }

        [Test]
        public void ProcessIncoming_MapToNull_Nre()
        {
            _mapper.Setup(m => m.Map<Request>(It.IsAny<Message>())).Returns(() => null);


            Assert.ThrowsAsync<NullReferenceException>(async () => await _target.ProcessIncomingAsync(It.IsAny<Message>()));


            _mockRepository.VerifyAll();
        }

        [Test]
        public async Task ProcessIncoming_MapToRequest_Success()
        {
            var request = _fixture.Create<Request>();

            _mapper.Setup(m => m.Map<Request>(It.IsAny<Message>())).Returns(request);

            var chatId = _fixture.Create<long>();

            var response = _fixture.Build<Response>()
                .With(r => r.ResponseText)
                .With(r => r.ChatHash, chatId.ToString())
                .OmitAutoProperties()
                .Create();

            _conversationService.Setup(s => s.GetResponseAsync(request)).ReturnsAsync(response);

            var message = _fixture.Build<Message>()
                .With(m => m.ClientId)
                .OmitAutoProperties()
                .Create();


            await _target.ProcessIncomingAsync(message);


            _mockRepository.VerifyAll();
        }

        [Test]
        public async Task ProcessIncoming_SendTextMessageAsync_Success()
        {
            var request = _fixture.Create<Request>();

            _mapper.Setup(m => m.Map<Request>(It.IsAny<Message>())).Returns(request);

            var chatId = _fixture.Create<long>();

            var response = _fixture.Build<Response>()
                .With(r => r.ResponseText)
                .With(r => r.ChatHash, chatId.ToString())
                .OmitAutoProperties()
                .Create();

            _conversationService.Setup(s => s.GetResponseAsync(request)).ReturnsAsync(response);

            var message = _fixture.Build<Message>()
                .With(m => m.ClientId)
                .OmitAutoProperties()
                .Create();


            await _target.ProcessIncomingAsync(message);


            _mockRepository.VerifyAll();
            _client.Verify(c => c.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>()));
        }

        [Test]
        public async Task ProcessIncoming_SendTextMessageAsyncThrows_Success()
        {
            var request = _fixture.Create<Request>();

            _mapper.Setup(m => m.Map<Request>(It.IsAny<Message>())).Returns(request);

            var chatId = _fixture.Create<long>();

            var response = _fixture.Build<Response>()
                .With(r => r.ResponseText)
                .With(r => r.ChatHash, chatId.ToString())
                .OmitAutoProperties()
                .Create();

            _conversationService.Setup(s => s.GetResponseAsync(request)).ReturnsAsync(response);

            _client.Setup(c => c.SendTextMessageAsync(It.IsAny<long>(), It.IsAny<string>())).Throws<Exception>();

            var message = _fixture.Build<Message>()
                .With(m => m.ClientId)
                .OmitAutoProperties()
                .Create();


            await _target.ProcessIncomingAsync(message);


            _mockRepository.VerifyAll();
        }

        [Test]
        public async Task SetWebhookAsync_SetWebhookAsync_Success()
        {
            var url = _fixture.Create<string>();


            await _target.SetWebhookAsync(url);


            _client.Verify(c => c.SetWebhookAsync(url));
        }

        [Test]
        public async Task DeleteWebHookAsync_DeleteWebHookAsync_Success()
        {
            await _target.DeleteWebhookAsync();


            _client.Verify(c => c.DeleteWebHookAsync());
        }
    }
}
