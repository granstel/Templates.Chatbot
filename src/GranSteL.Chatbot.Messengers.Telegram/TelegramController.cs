using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace GranSteL.Chatbot.Messengers.Telegram
{
    public class TelegramController : MessengerController<Update, string>
    {
        private readonly ITelegramService _telegramService;

        public TelegramController(ILogger<TelegramController> log, ITelegramService telegramService, TelegramConfiguration configuration)
            : base(log, telegramService, configuration)
        {
            _telegramService = telegramService;
        }

        [HttpGet("TestTelegramApi")]
        public async Task<IActionResult> TestTelegramApiAsync()
        {
            var result = await _telegramService.TestApiAsync();

            return Json(result);
        }

        [HttpGet("GetMe")]
        public async Task<IActionResult> GetMeAsync()
        {
            var user = await _telegramService.GetMeAsync();

            return new JsonResult(user);
        }
    }
}