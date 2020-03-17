using System;
using System.Runtime.Serialization;

namespace GranSteL.Chatbot.Api.Exceptions
{
    [Serializable]
    public class ExcludeBodyException : Exception
    {
        public ExcludeBodyException()
        {
        }

        public ExcludeBodyException(string message) : base(message)
        {
        }

        public ExcludeBodyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExcludeBodyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
