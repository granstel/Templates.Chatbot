using GranSteL.Chatbot.Services.Extensions;
using AutoFixture;
using NUnit.Framework;

namespace GranSteL.Chatbot.Services.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        private readonly Fixture _fixture = new Fixture();

        #region Sanitize

        [Test]
        public void Sanitize_Null_Null()
        {
            string expected = null;


            // ReSharper disable once ExpressionIsAlwaysNull
            var result = expected.Sanitize();


            Assert.Null(result);
        }

        [Test]
        public void Sanitize_Empty_Empty()
        {
            var expected = string.Empty;


            var result = expected.Sanitize();


            Assert.True(string.IsNullOrEmpty(result));
        }
                    
        [Test]      
        public void Sanitize_AnyString_Same()
        {
            var expected = _fixture.Create<string>();


            var result = expected.Sanitize();


            Assert.AreEqual(expected, result);
        }
                    
        [Test]      
        public void Sanitize_QuotesAtAnswer_Success()
        {
            var tested = "This text is with &quot;quotes&quot;";


            var result = tested.Sanitize();


            var expected = "This text is with \"quotes\"";
            Assert.AreEqual(expected, result);
        }

        #endregion Sanitize
    }
}
