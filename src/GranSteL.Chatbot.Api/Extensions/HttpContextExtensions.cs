using System;
using Microsoft.AspNetCore.Http;

namespace GranSteL.Chatbot.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool ContainsEndpoint(this HttpContext context, string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                return false;
            }

            return context.Request?.Path.Value != null
                   && context.Request.Path.Value.Contains(endpoint, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}