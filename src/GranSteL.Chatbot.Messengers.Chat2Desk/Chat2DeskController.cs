using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using Microsoft.AspNetCore.Mvc;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    [Produces("application/json")]
    [Route("/Chat2Desk")]
    public class Chat2DeskController : MessengerController<Message, string>
    {
        public Chat2DeskController(IChat2DeskService chat2DeskService, Chat2DeskConfiguration configuration) : base(chat2DeskService, configuration)
        {
        }
    }
}