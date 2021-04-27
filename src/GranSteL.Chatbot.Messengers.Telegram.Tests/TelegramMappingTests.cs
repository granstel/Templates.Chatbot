using AutoFixture;
using AutoMapper;
using GranSteL.Chatbot.Models;
using NUnit.Framework;
using Telegram.Bot.Types;

namespace GranSteL.Chatbot.Messengers.Telegram.Tests
{
    [TestFixture]
    public class TelegramProfileTests
    {
        private IMapper _target;

        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _target = new Mapper(new MapperConfiguration(c => c.AddProfile<TelegramMapping>()));

            _fixture = new Fixture { OmitAutoProperties = true };
        }

        [Test]
        public void ValidateConfiguration_Success()
        {
            _target.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_UpdateToRequest_Success()
        {
            var chat = _fixture.Build<Chat>()
                .With(c => c.Id)
                .Create();

            var from = _fixture.Build<User>()
                .With(u => u.Id)
                .Create();

            var message = _fixture.Build<Message>()
                .With(m => m.Chat, chat)
                .With(m => m.From, from)
                .With(m => m.Text)
                .Create();

            var update = _fixture.Build<Update>()
                .With(u => u.Message, message)
                .Create();

            var result = _target.Map<Request>(update);

            Assert.AreEqual((update.Message?.Chat?.Id).GetValueOrDefault().ToString(), result.ChatHash);
            Assert.AreEqual((update.Message?.From?.Id).GetValueOrDefault().ToString(), result.UserHash);
            Assert.AreEqual(update.Message?.Text, result.Text);
            Assert.AreEqual(Source.Telegram, result.Source);
        }
    }
}
