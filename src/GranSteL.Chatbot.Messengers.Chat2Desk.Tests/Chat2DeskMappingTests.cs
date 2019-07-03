using AutoMapper;
using NUnit.Framework;

namespace GranSteL.Chatbot.Messengers.Chat2Desk.Tests
{
    [TestFixture]
    public class Chat2DeskMappingTests
    {
        private IMapper _target;

        [SetUp]
        public void InitTest()
        {
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<Chat2DeskMapping>()));
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
