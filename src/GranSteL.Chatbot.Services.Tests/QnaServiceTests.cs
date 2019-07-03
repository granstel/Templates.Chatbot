using System;
using System.Linq;
using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Qna;
using GranSteL.Chatbot.Services.Configuration;
using AutoFixture;
using Moq;
using NUnit.Framework;

namespace GranSteL.Chatbot.Services.Tests
{
    [TestFixture]
    public class QnaServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<IQnaClient> _client;
        private Mock<QnaConfiguration> _configuration;

        private QnaService _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Default);

            _client = _mockRepository.Create<IQnaClient>();
            _configuration = _mockRepository.Create<QnaConfiguration>();

            _target = new QnaService(_client.Object, _configuration.Object);

            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAnswer_NullQuestion_Success()
        {
            var result = await _target.GetAnswerAsync(null);


            var expected = "Не задан вопрос";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_EmptyQuestion_Success()
        {
            var question = string.Empty;


            var result = await _target.GetAnswerAsync(question);


            var expected = "Не задан вопрос";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_NullKnowledgeBase_Success()
        {
            var question = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => null);


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var expected = "Возникла ошибка: Не удалось найти базу знаний";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_KnowledgeBase_Question_Used()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);


            await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();
            _client.Verify(c => c.GetAnswerAsync(knowledgeBase, question));
        }

        [Test]
        public async Task GetAnswer_Exception_Success()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);

            _client.Setup(c => c.GetAnswerAsync(knowledgeBase, question)).Throws<Exception>();


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var expected = "Возникла ошибка: Не удалось получить ответ";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_NullResponse_Success()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);

            _client.Setup(c => c.GetAnswerAsync(knowledgeBase, question)).ReturnsAsync(() => null);


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var expected = "Возникла ошибка: не получен ответ";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_ResponseWithErrorAndAnswers_Success()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);

            var response = _fixture.Build<Response>()
                .With(r => r.Error)
                .With(r => r.Answers)
                .Create();

            _client.Setup(c => c.GetAnswerAsync(knowledgeBase, question)).ReturnsAsync(() => response);


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var expected = $"Возникла ошибка: {response?.Error.Message}";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_ResponseWithAnswer_Success()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);

            var response = _fixture.Build<Response>()
                .With(r => r.Answers)
                .Without(r => r.Error)
                .Create();

            _client.Setup(c => c.GetAnswerAsync(knowledgeBase, question)).ReturnsAsync(() => response);


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var maxScore = response.Answers.Max(a => a.Score);
            var expected = response.Answers.FirstOrDefault(a => Math.Abs(maxScore - a.Score) < 1)?.Answer;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_QuotesAtAnswer_Success()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);

            var answer = _fixture.Build<AnswerModel>()
                .With(a => a.Answer, "Hello &quot;quot&quot;")
                .With(a => a.Score)
                .Without(a => a.Questions)
                .Create();

            var response = _fixture.Build<Response>()
                .With(r => r.Answers, new[] { answer })
                .Without(r => r.Error)
                .Create();

            _client.Setup(c => c.GetAnswerAsync(knowledgeBase, question)).ReturnsAsync(() => response);


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var expected = "Hello \"quot\"";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public async Task GetAnswer_ResponseNoAnswer_WithAnyScore_Success()
        {
            var question = _fixture.Create<string>();
            var knowledgeBase = _fixture.Create<string>();

            _configuration.SetupGet(c => c.KnowledgeBase).Returns(() => knowledgeBase);


            var answer = _fixture.Build<AnswerModel>()
                .With(a => a.Answer, "No good match found in the KB")
                .With(a => a.Score)
                .Without(a => a.Questions)
                .Create();

            var response = _fixture.Build<Response>()
                .With(r => r.Answers, new[] { answer })
                .Without(r => r.Error)
                .Create();

            _client.Setup(c => c.GetAnswerAsync(knowledgeBase, question)).ReturnsAsync(() => response);


            var result = await _target.GetAnswerAsync(question);


            _mockRepository.VerifyAll();

            var expected = "No good match found in the KB";

            Assert.AreEqual(expected, result);
        }
    }
}

