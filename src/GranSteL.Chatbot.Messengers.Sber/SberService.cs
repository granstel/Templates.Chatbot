using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Services;
using Sber.SmartApp.Models;

namespace GranSteL.Chatbot.Messengers.Sber
{
    public class SberService : MessengerService<Request, Response>, ISberService
    {
        private readonly IMapper _mapper;

        public SberService(
            IConversationService conversationService,
            IMapper mapper) : base(conversationService, mapper)
        {
            _mapper = mapper;
        }

        protected override async Task<Response> AfterAsync(Request input, Models.Response response)
        {
            var output = await base.AfterAsync(input, response);

            _mapper.Map(input, output);

            return output;
        }
    }
}
