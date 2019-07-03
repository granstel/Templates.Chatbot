using System.Net;
using System.Threading.Tasks;
using GranSteL.Chatbot.Services.Clients;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Chatbot.Services.Serialization;
using NUnit.Framework;
using RestSharp;

namespace GranSteL.Chatbot.Services.Tests.Integration
{
    [TestFixture]
    public class QnaServiceTests
    {
        private QnaService _target;

        [SetUp]
        public void InitTest()
        {
            var webClient = new RestClient { Proxy = new WebProxy("127.0.0.1", 8888) };

            var configuration = new QnaConfiguration
            {
                UrlFormat = "https://westus.api.cognitive.microsoft.com/qnamaker/v2.0/knowledgebases/{0}/generateAnswer",
                Token = "%CHATBOT_QNA_TOKEN%",
                KnowledgeBase = "your-kb-id"
            };

            var client = new QnaClient(webClient, configuration, new CustomJsonSerializer());

            _target = new QnaService(client, configuration);
        }

        [Test]
        [Ignore("Integration")]
        public async Task GetAnswer_Answer_Success()
        {
            var question = "hi";


            var answer = await _target.GetAnswerAsync(question);


            Assert.NotNull(answer);
        }
    }
}
