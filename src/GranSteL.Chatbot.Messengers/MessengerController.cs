using System;
using System.Linq;
using System.Threading.Tasks;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Chatbot.Services.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GranSteL.Chatbot.Messengers
{
    [Route("[controller]")]
    [Produces("application/json")]
    public abstract class MessengerController<TInput, TOutput> : Controller
    {
        private readonly IMessengerService<TInput, TOutput> _messengerService;
        private readonly MessengerConfiguration _configuration;

        protected readonly ILogger Log;
        protected JsonSerializerSettings SerializerSettings;

        private const string TokenParameter = "token";

        protected MessengerController(ILogger log, IMessengerService<TInput, TOutput> messengerService, MessengerConfiguration configuration)
        {
            Log = log;
            _messengerService = messengerService;
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isValid = IsValidRequest(context);

            if (!isValid)
            {
                context.Result = NotFound();
            }
        }

        [HttpGet]
        public string GetInfo()
        {
            var url = GetWebHookUrl(Request);

            return $"{DateTime.Now:F} {url}";
        }

        [HttpPost("{token?}")]
        public virtual async Task<IActionResult> WebHook([FromBody]TInput input, string token)
        {
            if (!ModelState.IsValid)
            {
                var errors = GetErrors(ModelState);
                Log.LogError(errors);
            }

            var response = await _messengerService.ProcessIncomingAsync(input);

            return Json(response, SerializerSettings);
        }

        [HttpPut("{token?}")]
        public virtual async Task<IActionResult> CreateWebHook(string token)
        {
            var url = GetWebHookUrl(Request);

            var result = await _messengerService.SetWebhookAsync(url);

            return Json(result);
        }

        [HttpDelete("{token?}")]
        public virtual async Task<IActionResult> DeleteWebHook(string token)
        {
            var result = await _messengerService.DeleteWebhookAsync();

            return Json(result);
        }

        protected virtual bool IsValidRequest(ActionExecutingContext context)
        {
            var actionHasTokenParameter = context.ActionDescriptor.Parameters.Any(p => string.Equals(p.Name, TokenParameter));

            if (string.IsNullOrEmpty(_configuration.IncomingToken) || !actionHasTokenParameter)
            {
                return true;
            }

            context.ActionArguments.TryGetValue(TokenParameter, out object value);

            var token = value as string;

            return string.Equals(_configuration.IncomingToken, token, StringComparison.InvariantCultureIgnoreCase);
        }

        private string GetWebHookUrl(HttpRequest request)
        {
            var pathBase = request.PathBase.Value;
            var pathSegment = request.Path.Value;

            var url = $"{request.Scheme}://{request.Host}{pathBase}{pathSegment}";

            return url;
        }

        private string GetErrors(ModelStateDictionary modelState)
        {
            return modelState?.Values
                .SelectMany(v => v.Errors?.Select(e => e.ErrorMessage))
                .Where(m => !string.IsNullOrEmpty(m))
                .JoinToString(Environment.NewLine);
        }
    }
}
