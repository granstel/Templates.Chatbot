using System;
using System.Threading.Tasks;
using AutoMapper;
using FillInTheTextBot.Services;
using FillInTheTextBot.Services.Extensions;
using GranSteL.Helpers.Redis;
using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Input;

namespace FillInTheTextBot.Messengers.Marusia
{
    public class MarusiaService : MessengerService<InputModel, OutputModel>, IMarusiaService
    {
        private readonly IMapper _mapper;

        public MarusiaService(
            IConversationService conversationService,
            IMapper mapper,
            IDialogflowService dialogflowService,
            IRedisCacheService cache) : base(conversationService, mapper, dialogflowService)
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
