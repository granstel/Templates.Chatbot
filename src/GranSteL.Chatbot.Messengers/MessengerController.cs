using System;
using System.Threading.Tasks;
using GranSteL.Chatbot.Messengers.Extensions;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace GranSteL.Chatbot.Messengers
{
    public abstract class MessengerController<TInput, TOutput> : Controller
    {
        private readonly IMessengerService<TInput, TOutput> _messengerService;
        private readonly MessengerConfiguration _configuration;
        
        private readonly Logger _log;

        private const string TokenParameter = "token";

        protected MessengerController(IMessengerService<TInput, TOutput> messengerService, MessengerConfiguration configuration)
        {
            _messengerService = messengerService;
            _configuration = configuration;

            _log = LogManager.GetLogger(GetType().Name);
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
            var url = this.GetWebHookUrl();

            return $"{DateTime.Now:F} {url}";
        }

        [HttpPost("{token?}")]
        public virtual async Task<IActionResult> WebHook([FromBody]TInput input, string token)
        {
            var response = await _messengerService.ProcessIncomingAsync(input);

            return Json(response);
        }

        [HttpPut("{token?}")]
        public virtual async Task<IActionResult> CreateWebHook(string token)
        {
            var url = this.GetWebHookUrl();

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
            if (string.IsNullOrEmpty(_configuration.IncomingToken))
            {
                return true;
            }

            if (context.ActionArguments.TryGetValue(TokenParameter, out object value))
            {
                var token = value as string;

                return string.Equals(_configuration.IncomingToken, token, StringComparison.InvariantCultureIgnoreCase);
            }

            return true;
        }
    }
}
