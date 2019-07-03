using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Models.Yandex;
using GranSteL.Chatbot.Services;
using Internal = GranSteL.Chatbot.Models.Internal;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    public class YandexService : MessengerService<InputModel, OutputModel>, IYandexService
    {
        private readonly IMapper _mapper;

        public YandexService(IConversationService conversationService, IMapper mapper) : base(conversationService, mapper)
        {
            _mapper = mapper;
        }

        protected override async Task<OutputModel> AfterAsync(InputModel input, Internal.Response response)
        {
            var output = await base.AfterAsync(input, response);

            _mapper.Map(input, output);

            return output;
        }
    }
}
