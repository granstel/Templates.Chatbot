using System;
using System.Net;
using System.Threading.Tasks;
using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using GranSteL.Chatbot.Services.Extensions;
using NLog;
using RestSharp;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class Chat2DeskClient : IChat2DeskClient
    {
        private readonly IRestClient _webClient;
        private readonly Chat2DeskConfiguration _configuration;
        private readonly CustomJsonSerializer _serializer;

        private readonly Logger _log = LogManager.GetLogger(nameof(Chat2DeskClient));

        public Chat2DeskClient(IRestClient webClient, Chat2DeskConfiguration configuration, CustomJsonSerializer serializer)
        {
            _webClient = webClient;
            _configuration = configuration;
            _serializer = serializer;

            _webClient.BaseUrl = new Uri(_configuration.Url);
        }

        public async Task<bool> SetWebhookAsync(string url)
        {
            var restRequest = new RestRequest("/companies/web_hook", Method.POST);
            restRequest.AddHeader("Authorization", _configuration.Token);

            var request = new WebHook
            {
                Url = url,
                Events = new[] { "inbox" }
            };

            restRequest.JsonSerializer = _serializer;

            restRequest.AddJsonBody(request);

            try
            {
                var response = await _webClient.ExecuteAsync(restRequest);

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _log.Error(e, "При создании web hook возникла ошибка");

                return false;
            }
        }

        public async Task<bool> DeleteWebHookAsync()
        {
            var restRequest = new RestRequest("/companies/web_hook", Method.POST);
            restRequest.AddHeader("Authorization", _configuration.Token);

            var request = new WebHook();

            restRequest.JsonSerializer = _serializer;

            restRequest.AddJsonBody(request);

            try
            {
                var response = await _webClient.ExecuteAsync(restRequest);

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                _log.Error(e, "При удалении web hook возникла ошибка");

                return false;
            }
        }

        public async Task<bool> SendTextMessageAsync(long chatId, string text)
        {
            var restRequest = new RestRequest("/messages", Method.POST);
            restRequest.AddHeader("Authorization", _configuration.Token);

            var message = new Message
            {
                Text = text,
                ClientId = chatId
            };

            restRequest.JsonSerializer = _serializer;

            restRequest.AddJsonBody(message);

            try
            {
                var response = await _webClient.ExecuteAsync(restRequest);

                var info = response?.Content.Deserialize<SendInformation>();

                return string.Equals("success", info?.Status, StringComparison.InvariantCultureIgnoreCase);
            }
            catch (Exception e)
            {
                _log.Error(e, "При отправлении сообщения возникла ошибка");

                return false;
            }
        }
    }
}
