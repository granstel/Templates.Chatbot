using System;
using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Models;
using GranSteL.Chatbot.Services;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Messengers
{
    public abstract class MessengerService<TInput, TOutput> : IMessengerService<TInput, TOutput>
    {
        protected readonly ILogger Log;

        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;

        protected MessengerService(ILogger log, IConversationService conversationService, IMapper mapper)
        {
            Log = log;
            _conversationService = conversationService;
            _mapper = mapper;
        }

        protected virtual Request Before(TInput input)
        {
            throw new NotImplementedException(
                $"Need to map {typeof(TInput)} type to {typeof(Request)} type at overrided '{nameof(Before)}' method at {this.GetType().FullName} type");
        }

        public virtual async Task<TOutput> ProcessIncomingAsync(TInput input)
        {
            Response response;

            try
            {
                var request = Before(input);

                response = ProcessCommand(request);

                if (response == null)
                {
                    response = await _conversationService.GetResponseAsync(request);
                }

                _mapper.Map(request, response);
            }
            catch (Exception e)
            {
                Log.LogError(e, "Error while processing incoming message");

                response = new Response
                {
                    Text = "Прости, у меня какие-то проблемы... Давай попробуем ещё раз"
                };
            }

            var output = await AfterAsync(input, response);

            return output;
        }

        protected virtual Response ProcessCommand(Request request)
        {
            return null;
        }

        protected virtual async Task<TOutput> AfterAsync(TInput input, Response response)
        {
            return await Task.Run(() =>
            {
                var output = _mapper.Map<TOutput>(response);

                return output;
            });
        }

        public virtual Task<bool> SetWebhookAsync(string url)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> DeleteWebhookAsync()
        {
            throw new NotImplementedException();
        }
    }
}
