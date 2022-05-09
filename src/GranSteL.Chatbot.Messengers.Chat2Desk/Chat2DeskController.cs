using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class Chat2DeskController : MessengerController<Message, string>
    {
        public Chat2DeskController(ILogger<Chat2DeskController> log, IChat2DeskService chat2DeskService, Chat2DeskConfiguration configuration)
            : base(log, chat2DeskService, configuration)
        {
        }
    }
}