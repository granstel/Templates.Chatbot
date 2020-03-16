using System.Collections.Generic;
using System.Linq;

namespace GranSteL.Chatbot.Services.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinToString<T>(this IEnumerable<T> source, string separator = ", ")
        {
            if (source == null)
                return null;

            var list = source.ToList();

            var result = string.Join(separator, list);

            return result;
        }
    }
}
