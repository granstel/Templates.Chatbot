using System.Threading.Tasks;
using AutoMapper;
using GranSteL.Chatbot.Services;
using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Input;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Messengers.Marusia
{
    public class MarusiaService : MessengerService<InputModel, OutputModel>, IMarusiaService
    {
        private readonly IMapper _mapper;

        public MarusiaService(
            ILogger<MarusiaService> log,
            IConversationService conversationService,
            IMapper mapper) : base(log, conversationService, mapper)
        {
            _mapper = mapper;
        }

        protected override async Task<OutputModel> AfterAsync(InputModel input, Models.Response response)
        {
            var output = await base.AfterAsync(input, response);

            _mapper.Map(input, output);

            return output;
        }
    }
}
