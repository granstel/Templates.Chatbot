using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
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
            var loggerMock = Mock.Of<ILogger<SberMapping>>();
            var profile = new SberMapping(loggerMock);
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile(profile)));
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
