using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Services
{
    public static class InternalLoggerFactory
    {
        public static ILoggerFactory Factory { get; set; }

        public static ILogger<T> CreateLogger<T>() => Factory?.CreateLogger<T>();

        public static ILogger CreateLogger(string categoryName) => Factory?.CreateLogger(categoryName);
    }
}