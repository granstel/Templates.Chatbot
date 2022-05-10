using System;
using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using GranSteL.Chatbot.Models;
using GranSteL.Chatbot.Services;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class Chat2DeskService : MessengerService<Message, string>, IChat2DeskService
    {
        private readonly IChat2DeskClient _client;

        public Chat2DeskService(
            ILogger<Chat2DeskService> log,
            IChat2DeskClient client,
            IConversationService conversationService,
            IMapper mapper) : base(log, conversationService, mapper)
        {
            _client = client;
        }

        protected override async Task<string> AfterAsync(Message input, Response response)
        {
            var answer = response.Text;

            await SendTextMessageAsync(long.Parse(response.ChatHash), answer);

            return answer;
        }

        public override async Task<bool> SetWebhookAsync(string url)
        {
            var result = await _client.SetWebhookAsync(url);

            return result;
        }

        public override async Task<bool> DeleteWebhookAsync()
        {
            var result = await _client.DeleteWebHookAsync();

            return result;
        }

        public async Task SendTextMessageAsync(long chatId, string text)
        {
            try
            {
                await _client.SendTextMessageAsync(chatId, text);
            }
            catch (Exception e)
            {
                Log.LogError(e, "Error while send text message");
            }
        }
    }
}
