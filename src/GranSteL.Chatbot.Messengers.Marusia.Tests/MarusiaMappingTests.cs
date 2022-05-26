using AutoFixture;
using AutoFixture.Kernel;
using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Buttons;
using MailRu.Marusia.Models.Cards;
using MailRu.Marusia.Models.Input;
using NUnit.Framework;

namespace GranSteL.Chatbot.Messengers.Marusia.Tests
{
    [TestFixture]
    public class MarusiaMappingTests
    {
        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new TypeRelay(typeof(Button), typeof(ResponseButton)));
            _fixture.Customizations.Add(new TypeRelay(typeof(ICard), typeof(ItemsListCard)));
        }

        [Test]
        public void Map_InputToOutput()
        {
            var input = _fixture.Create<InputModel>();

            var output = _fixture.Build<OutputModel>()
                .Without(o => o.Session)
                .Without(o => o.Version)
                .Create();


            output = input.FillOutput(output);


            Assert.AreEqual(input.Session.SessionId, output.Session.SessionId);
            Assert.AreEqual(input.Session.MessageId, output.Session.MessageId);
            Assert.AreEqual(input.Version, output.Version);
            Assert.NotNull(output.Response);
        }
    }
}
