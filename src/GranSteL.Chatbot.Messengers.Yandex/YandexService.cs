using System;
using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Services;
using Yandex.Dialogs.Models;
using Yandex.Dialogs.Models.Input;
using InternalModels = GranSteL.Chatbot.Models;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public class YandexService : MessengerService<InputModel, OutputModel>, IYandexService
    {
        private const string PingCommand = "ping";
        private const string PongResponse = "pong";

        private readonly IMapper _mapper;


        public YandexService(IConversationService conversationService, IMapper mapper) : base(conversationService, mapper)
        {
            _mapper = mapper;
        }

        protected override InternalModels.Response ProcessCommand(InternalModels.Request request)
        {
            InternalModels.Response response = null;

            if (PingCommand.Equals(request.Text, StringComparison.InvariantCultureIgnoreCase))
            {
                response = new InternalModels.Response { Text = PongResponse };
            }

            return response;
        }

        protected override async Task<OutputModel> AfterAsync(InputModel input, InternalModels.Response response)
        {
            var output = await base.AfterAsync(input, response);

            _mapper.Map(input, output);

            return output;
        }
    }
}
