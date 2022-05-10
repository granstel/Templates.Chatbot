using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Api.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly ILogger<ExceptionsMiddleware> _log;
        private readonly RequestDelegate _next;

        public ExceptionsMiddleware(ILogger<ExceptionsMiddleware> log, RequestDelegate next)
        {
            _log = log;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error while processing request");

                throw;
            }
        }
    }
}
