using System;
using System.Linq;
using System.Threading.Tasks;
using GranSteL.Chatbot.Services.Clients;
using GranSteL.Chatbot.Services.Configuration;
using AutoFixture;
using GranSteL.Chatbot.Services.Serialization;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace GranSteL.Chatbot.Services.Tests.Clients
{
    [TestFixture]
    public class QnaClientTests
    {
        private MockRepository _mockRepository;

        private Mock<IRestClient> _webClient;
        private Mock<QnaConfiguration> _configuration;

        private QnaClient _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _webClient = _mockRepository.Create<IRestClient>();
            _configuration = _mockRepository.Create<QnaConfiguration>();

            _configuration.SetupGet(c => c.UrlFormat).Returns("http://test.test/{0}");

            _target = new QnaClient(_webClient.Object, _configuration.Object, new CustomJsonSerializer());

            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAnswerAsync_Exception_Null()
        {
            var knowledgeBase = _fixture.Create<string>();
            var question = _fixture.Create<string>();

            _configuration.SetupGet(c => c.Token).Returns(_fixture.Create<string>());

            _webClient.Setup(c => c.ExecuteTaskAsync(It.IsAny<RestRequest>())).Throws<Exception>();

            
            var result = await _target.GetAnswerAsync(knowledgeBase, question);


            _mockRepository.VerifyAll();

            Assert.Null(result);
        }

        [Test]
        public async Task GetAnswerAsync_UrlFormat_Used()
        {
            var knowledgeBase = _fixture.Create<string>();
            var urlFormat = "http://test/{0}";

            _configuration.SetupGet(c => c.UrlFormat).Returns(urlFormat);


            await _target.GetAnswerAsync(knowledgeBase, It.IsAny<string>());


            _mockRepository.VerifyAll();

            var expected = new Uri(string.Format(urlFormat, knowledgeBase));
            _webClient.VerifySet(c => c.BaseUrl = expected);
        }

        [Test]
        public async Task GetAnswerAsync_Token_Used()
        {
            var token = _fixture.Create<string>();

            _configuration.SetupGet(c => c.Token).Returns(token);

            var request = default(IRestRequest);
            _webClient.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>())).Callback((IRestRequest r) => request = r);


            await _target.GetAnswerAsync(It.IsAny<string>(), It.IsAny<string>());


            _mockRepository.VerifyAll();

            var header = request.Parameters.FirstOrDefault(p => string.Equals("Ocp-Apim-Subscription-Key", p.Name))?.Value;
            Assert.AreEqual(token, header);
        }

        [Test]
        public async Task GetAnswerAsync_Question_Used()
        {
            var question = _fixture.Create<string>();

            var request = default(IRestRequest);
            _webClient.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>())).Callback((IRestRequest r) => request = r);


            await _target.GetAnswerAsync(It.IsAny<string>(), question);


            _mockRepository.VerifyAll();

            var body = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody)?.Value.ToString();

            Assert.NotNull(body);
            Assert.True(body.Contains(question));
        }

        [Test]
        public async Task GetAnswerAsync_ExecuteTaskAsync_Success()
        {
            var question = _fixture.Create<string>();


            await _target.GetAnswerAsync(It.IsAny<string>(), question);


            _mockRepository.VerifyAll();
            _webClient.Verify(c => c.ExecuteTaskAsync(It.IsAny<RestRequest>()));
        }
    }
}
