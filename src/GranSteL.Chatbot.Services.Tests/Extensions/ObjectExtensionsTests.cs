using GranSteL.Chatbot.Services.Extensions;
using AutoFixture;
using GranSteL.Chatbot.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GranSteL.Chatbot.Services.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTests
    {
        private Fixture _fixture;

        [SetUp]
        public void InitTest()
        {
            _fixture = new Fixture { OmitAutoProperties = true };
        }

        [Test]
        public void Serialize_String_NotSerialize()
        {
            var expected = _fixture.Create<string>();


            var result = expected.Serialize();


            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Serialize_Object_Serialize()
        {
            var obj = _fixture.Create<object>();
            var expected = JsonConvert.SerializeObject(obj);


            var result = obj.Serialize();


            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Deserialize_Throws_Default()
        {
            var serialized = _fixture.Create<string>();


            var result = serialized.Deserialize<Request>();


            Assert.AreEqual(null, result);
        }
    }
}
