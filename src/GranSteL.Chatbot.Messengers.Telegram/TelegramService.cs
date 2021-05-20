using System;
using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Models;
using GranSteL.Chatbot.Services;
using NLog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace GranSteL.Chatbot.Messengers.Telegram
{
    public class TelegramService : MessengerService<Update, string>, ITelegramService
    {
        private readonly ITelegramBotClient _client;

        private readonly Logger _log = LogManager.GetLogger(nameof(TelegramService));

        public TelegramService(ITelegramBotClient client, IConversationService conversationService, IMapper mapper) : base(conversationService, mapper)
        {
            _client = client;
        }

        public async Task<User> GetMeAsync()
        {
            var result = await _client.GetMeAsync();

            return result;
        }

        public async Task<bool> TestApiAsync()
        {
            var result = await _client.TestApiAsync();

            return result;
        }

        public override async Task<bool> SetWebhookAsync(string url)
        {
            try
            {
                await _client.SetWebhookAsync(url);
            }
            catch (Exception e)
            {
                _log.Error(e);

                return false;
            }

            return true;
        }

        public override async Task<bool> DeleteWebhookAsync()
        {
            try
            {
                await _client.DeleteWebhookAsync();

                return true;
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return false;
        }

        protected override async Task<string> AfterAsync(Update input, Response response)
        {
            var answer = response.Text;

            await SendTextMessageAsync(long.Parse(response.ChatHash), answer);

            return answer;
        }

        private async Task SendTextMessageAsync(long chatId, string text)
        {
            try
            {
                await _client.SendTextMessageAsync(chatId, text);
            }
            catch (Exception e)
            {
                _log.Warn(e);
            }
        }
    }
}
