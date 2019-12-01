using System;
using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Dialogflow.V2;
using GranSteL.Chatbot.Models.Internal;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Chatbot.Services.Extensions;
using NLog;

namespace GranSteL.Chatbot.Services
{
    public class DialogflowService : IDialogflowService
    {
        private readonly Logger _log = LogManager.GetLogger(nameof(DialogflowService));

        private readonly SessionsClient _dialogflowClient;
        private readonly DialogflowConfiguration _configuration;
        private readonly IMapper _mapper;

        public DialogflowService(SessionsClient dialogflowClient, DialogflowConfiguration configuration, IMapper mapper)
        {
            _dialogflowClient = dialogflowClient;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Dialog> GetResponseAsync(Request request)
        {
            var intentRequest = CreateQuery(request);

            _log.Trace($"Request:{Environment.NewLine}{intentRequest.Serialize()}");

            var intentResponse = await _dialogflowClient.DetectIntentAsync(intentRequest);

            _log.Trace($"Response:{Environment.NewLine}{intentResponse.Serialize()}");

            var queryResult = intentResponse.QueryResult;

            var response = _mapper.Map<Dialog>(queryResult);

            return response;
        }

        private DetectIntentRequest CreateQuery(Request request)
        {
            var session = new SessionName(_configuration.ProjectId, request.SessionId);

            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = request.Text,
                    LanguageCode = _configuration.LanguageCode
                },
            };

            var intentRequest = new DetectIntentRequest
            {
                SessionAsSessionName = session,
                QueryInput = query
            };

            return intentRequest;
        }
    }
}
