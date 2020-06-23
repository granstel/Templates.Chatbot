using AutoFixture;
using AutoMapper;
using NUnit.Framework;
using Yandex.Dialogs.Models;
using Yandex.Dialogs.Models.Input;

namespace GranSteL.Chatbot.Messengers.Yandex.Tests
{
    [TestFixture]
    public class YandexProfileTests
    {
        private IMapper _target;
                
        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<YandexProfile>()));

            _fixture = new Fixture();
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_InputToOutput()
        {
            var input = _fixture.Create<InputModel>();

            var output = _fixture.Build<OutputModel>()
                .Without(o => o.Session)
                .Without(o => o.Version)
                .Create();


            _target.Map(input, output);


            Assert.AreEqual(input.Session.SessionId, output.Session.SessionId);
            Assert.AreEqual(input.Session.MessageId, output.Session.MessageId);
            Assert.AreEqual(input.Version, output.Version);
            Assert.NotNull(output.Response);
        }
    }
}
