using AutoMapper;
using GranSteL.Chatbot.Services.Mapping;
using NUnit.Framework;

namespace GranSteL.Chatbot.Services.Tests.MappingProfiles
{
    [TestFixture]
    public class DialogflowProfileTests
    {
        private IMapper _target;

        [SetUp]
        public void InitTest()
        {
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<DialogflowProfile>()));
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
