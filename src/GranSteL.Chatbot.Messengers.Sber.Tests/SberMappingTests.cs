using AutoMapper;
using NUnit.Framework;

namespace GranSteL.Chatbot.Messengers.Sber.Tests
{
    [TestFixture]
    public class SberMappingTests
    {
        private IMapper _target;

        [SetUp]
        public void InitTest()
        {
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<SberMapping>()));
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
