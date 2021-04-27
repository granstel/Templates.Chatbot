using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using GranSteL.Chatbot.Services;
using Moq;
using NUnit.Framework;
using Request = GranSteL.Chatbot.Models.Request;
using Response = GranSteL.Chatbot.Models.Response;

namespace GranSteL.Chatbot.Messengers.Marusia.Tests
{
    [TestFixture]
    public class YandexServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<IConversationService> _conversationService;
        private Mock<IMapper> _mapper;

        private YandexService _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _conversationService = _mockRepository.Create<IConversationService>();
            _mapper = _mockRepository.Create<IMapper>();

            _target = new YandexService(_conversationService.Object, _mapper.Object);

            _fixture = new Fixture();
        }

        [Test]
        public async Task ProcessIncomingAsync_Invokations_Success()
        {
            var inputModel = _fixture.Build<InputModel>()
                .OmitAutoProperties()
                .Create();

            var request = _fixture.Build<Request>()
                .OmitAutoProperties()
                .Create();

            _mapper.Setup(m => m.Map<Request>(It.IsAny<InputModel>())).Returns(request);

            _conversationService.Setup(s => s.GetResponseAsync(request)).ReturnsAsync(() => null);

            _mapper.Setup(m => m.Map(It.IsAny<Request>(), It.IsAny<Response>())).Returns(() => null);

            var output = _fixture.Build<OutputModel>()
                .With(o => o.Session)
                .OmitAutoProperties()
                .Create();

            _mapper.Setup(m => m.Map<OutputModel>(It.IsAny<Response>())).Returns(output);
            _mapper.Setup(m => m.Map(It.IsAny<InputModel>(), It.IsAny<OutputModel>())).Returns(() => null);


            var result = await _target.ProcessIncomingAsync(inputModel);


            _mockRepository.VerifyAll();

            Assert.NotNull(result);
        }
    }
}
