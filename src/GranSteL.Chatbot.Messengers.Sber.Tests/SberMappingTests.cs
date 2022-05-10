using AutoMapper;
using GranSteL.Chatbot.Services;
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
            InternalLoggerFactory.Factory = Mock.Of<ILoggerFactory>();

            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<SberMapping>()));
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
