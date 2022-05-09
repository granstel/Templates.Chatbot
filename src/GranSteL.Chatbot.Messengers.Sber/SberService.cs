using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Services;
using Microsoft.Extensions.Logging;
using Sber.SmartApp.Models;

namespace GranSteL.Chatbot.Messengers.Sber
{
    public class SberService : MessengerService<Request, Response>, ISberService
    {
        private readonly IMapper _mapper;

        public SberService(
            ILogger<SberService> log,
            IConversationService conversationService,
            IMapper mapper) : base(log, conversationService, mapper)
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
