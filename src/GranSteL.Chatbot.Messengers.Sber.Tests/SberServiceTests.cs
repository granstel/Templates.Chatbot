using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using GranSteL.Chatbot.Services;
using Moq;
using NUnit.Framework;
using Sber.SmartApp.Models;
using InternalModels = GranSteL.Chatbot.Models;

namespace GranSteL.Chatbot.Messengers.Sber.Tests
{
    [TestFixture]
    public class SberServiceTests
    {
        private MockRepository _mockRepository;

        private Mock<IConversationService> _conversationService;
        private Mock<IMapper> _mapper;

        private SberService _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _conversationService = _mockRepository.Create<IConversationService>();
            _mapper = _mockRepository.Create<IMapper>();

            _target = new SberService(_conversationService.Object, _mapper.Object);

            _fixture = new Fixture();
        }

        [Test]
        public async Task ProcessIncomingAsync_Invokations_Success()
        {
            var inputModel = _fixture.Build<Request>()
                .OmitAutoProperties()
                .Create();

            var request = _fixture.Build<InternalModels.Request>()
                .OmitAutoProperties()
                .Create();

            _mapper.Setup(m => m.Map<InternalModels.Request>(It.IsAny<Request>())).Returns(request);

            _conversationService.Setup(s => s.GetResponseAsync(request)).ReturnsAsync(() => null);

            _mapper.Setup(m => m.Map(It.IsAny<InternalModels.Request>(), It.IsAny<InternalModels.Response>())).Returns(() => null);

            var output = _fixture.Build<Response>()
                .OmitAutoProperties()
                .Create();

            _mapper.Setup(m => m.Map<Response>(It.IsAny<InternalModels.Response>())).Returns(output);
            _mapper.Setup(m => m.Map(It.IsAny<Request>(), It.IsAny<Response>())).Returns(() => null);


            var result = await _target.ProcessIncomingAsync(inputModel);


            _mockRepository.VerifyAll();

            Assert.NotNull(result);
        }
    }
}
