using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GranSteL.Chatbot.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NLog;

namespace GranSteL.Chatbot.Api.Middleware
{
    public class HttpLogMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public HttpLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // ReSharper disable once UnusedMember.Global
        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context.Request);

            var responseBody = context.Response.Body;

            using (var stream = new MemoryStream())
            {
                context.Response.Body = stream;

                try
                {
                    await _next(context);
                }
                finally
                {
                    await LogResponse(context.Response);

                    await stream.CopyToAsync(responseBody);

                    context.Response.Body = responseBody;
                }
            }
        }

        private async Task LogRequest(HttpRequest request)
        {
            _log.SetProperty("Type", "Request");

            var builder = new StringBuilder();

            var method = request.Method;
            var queryString = request.QueryString;


            builder.AppendLine($"{method} {request.Path}{queryString}");
            _log.SetProperty("Method", method);
            _log.SetProperty("QueryString", queryString.Value);

            var user = request.HttpContext?.User?.Identity?.Name;

            _log.SetProperty("User", user);
            builder.AppendLine($"User: {user}");

            AddHeaders(builder, request.Headers);

            if (request.ContentLength > 0)
            {
                request.EnableRewind();

                await AddBodyAsync(builder, request.Body);
            }

            var message = builder.ToString();

            _log.Info(message);

            ClearProperties();
        }

        private async Task LogResponse(HttpResponse response)
        {
            _log.SetProperty("Type", "Response");

            var builder = new StringBuilder();

            var statusCode = response.StatusCode;

            builder.AppendLine($"{statusCode}");
            _log.SetProperty("StatusCode", statusCode);

            builder.AppendLine($"{response.ContentType}");

            AddHeaders(builder, response.Headers);

            if (response.Body.Length > 0)
            {
                response.Body.Seek(0, SeekOrigin.Begin);

                await AddBodyAsync(builder, response.Body);
            }

            var message = builder.ToString();

            _log.Info(message);

            ClearProperties();
        }

        private void AddHeaders(StringBuilder builder, IHeaderDictionary headers)
        {
            foreach (var header in headers)
            {
                builder.AppendLine($"{header.Key}: {header.Value.JoinToString(" ")}");
            }

            var dictionary = headers.ToDictionary(c => c.Key, c => c.Value.JoinToString(","));

            _log.SetProperty("Headers", dictionary);
        }

        private async Task AddBodyAsync(StringBuilder builder, Stream body)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await body.CopyToAsync(memoryStream);

                    body.Seek(0, SeekOrigin.Begin);
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (var reader = new StreamReader(memoryStream))
                    {
                        var content = reader.ReadToEnd();

                        builder.AppendLine("Body: ");
                        builder.AppendLine(content);
                        _log.SetProperty("Body", content);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e, "Не удалось записать тело в лог");
            }
        }

        private void ClearProperties()
        {
            _log.SetProperty("Type", null);
            _log.SetProperty("Headers", null);
            _log.SetProperty("Body", null);
            _log.SetProperty("Method", null);
            _log.SetProperty("QueryString", null);
            _log.SetProperty("StatusCode", null);
            _log.SetProperty("User", null);
        }
    }
}
