using System;
using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Models;
using GranSteL.Chatbot.Services;
using NLog;

namespace GranSteL.Chatbot.Messengers
{
    public abstract class MessengerService<TInput, TOutput> : IMessengerService<TInput, TOutput>
    {
        protected readonly Logger Log;

        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;

        protected MessengerService(IConversationService conversationService, IMapper mapper)
        {
            Log = LogManager.GetLogger(GetType().Name);

            _conversationService = conversationService;
            _mapper = mapper;
        }

        protected virtual Request Before(TInput input)
        {
            var request = _mapper.Map<Request>(input);

            return request;
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
                Log.Error(e);

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
            throw new System.NotImplementedException();
        }

        public virtual Task<bool> DeleteWebhookAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
