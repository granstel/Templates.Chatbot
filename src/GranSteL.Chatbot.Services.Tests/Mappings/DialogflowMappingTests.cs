using AutoMapper;
using GranSteL.Chatbot.Services.Mappings;
using NUnit.Framework;

namespace GranSteL.Chatbot.Services.Tests.Mappings
{
    [TestFixture]
    public class DialogflowMappingTests
    {
        private IMapper _target;

        [SetUp]
        public void InitTest()
        {
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<DialogflowMapping>()));
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
