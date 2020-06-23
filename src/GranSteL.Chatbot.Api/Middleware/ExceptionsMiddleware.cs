using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;

namespace GranSteL.Chatbot.Api.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly Logger _log = LogManager.GetLogger(nameof(ExceptionsMiddleware));
        private readonly RequestDelegate _next;

        public ExceptionsMiddleware(RequestDelegate next)
        {
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
                _log.Error(ex);

                throw;
            }
        }
    }
}
