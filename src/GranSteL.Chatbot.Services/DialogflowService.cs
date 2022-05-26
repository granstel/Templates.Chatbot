using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Google.Cloud.Dialogflow.V2;
using GranSteL.Chatbot.Models;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Chatbot.Services.Extensions;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Services
{
    public class DialogflowService : IDialogflowService
    {
        private const string StartCommand = "/start";
        private const string ErrorCommand = "/error";

        private const string WelcomeEventName = "Welcome";
        private const string ErrorEventName = "Error";

        private readonly Dictionary<string, string> _commandDictionary = new Dictionary<string, string>
        {
            {StartCommand, WelcomeEventName},
            {ErrorCommand, ErrorEventName}
        };

        private readonly ILogger<DialogflowService> _log;

        private readonly SessionsClient _dialogflowClient;
        private readonly DialogflowConfiguration _configuration;
        private readonly IMapper _mapper;

        public DialogflowService(
            ILogger<DialogflowService> log,
            SessionsClient dialogflowClient,
            DialogflowConfiguration configuration,
            IMapper mapper)
        {
            _log = log;
            _dialogflowClient = dialogflowClient;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Dialog> GetResponseAsync(Request request)
        {
            var intentRequest = CreateQuery(request);

            if (_configuration.LogQuery)
                _log.LogTrace($"Request:{System.Environment.NewLine}{intentRequest.Serialize()}");

            var intentResponse = await _dialogflowClient.DetectIntentAsync(intentRequest);

            if (_configuration.LogQuery)
                _log.LogTrace($"Response:{System.Environment.NewLine}{intentResponse.Serialize()}");

            var queryResult = intentResponse.QueryResult;

            var response = _mapper.Map<Dialog>(queryResult);

            return response;
        }

        private DetectIntentRequest CreateQuery(Request request)
        {
            var session = new SessionName(_configuration.ProjectId, request.SessionId);

            var eventInput = ResolveEvent(request);

            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = request.Text,
                    LanguageCode = _configuration.LanguageCode
                },
            };

            if (eventInput != null)
            {
                query.Event = eventInput;
            }

            var intentRequest = new DetectIntentRequest
            {
                SessionAsSessionName = session,
                QueryInput = query,
                QueryParams = new QueryParameters()
            };

            var sourceContext = GetContext(_configuration.ProjectId, session, nameof(request.Source));
            var screenContext = GetContext(_configuration.ProjectId, session, nameof(request.HasScreen));
            var appealContext = GetContext(_configuration.ProjectId, session, nameof(request.Appeal));

            intentRequest.QueryParams.Contexts.AddRange(new[] { sourceContext, screenContext, appealContext });

            return intentRequest;
        }

        private EventInput ResolveEvent(Request request)
        {
            var result = default(EventInput);

            var requestText = request.Text;

            if (string.IsNullOrEmpty(requestText))
            {
                return GetEvent(WelcomeEventName);
            }

            if (_commandDictionary.TryGetValue(requestText, out var eventName))
            {
                return GetEvent(eventName);
            }

            return result;
        }

        private EventInput GetEvent(string name)
        {
            return new EventInput
            {
                Name = name,
                LanguageCode = _configuration.LanguageCode
            };
        }

        private Context GetContext(string projectId, SessionName sessionName, string contextName, int lifeSpan = 2, IDictionary<string, string> parameters = null)
        {
            var context = new Context
            {
                ContextName = new ContextName(projectId, sessionName.SessionId, contextName),
                LifespanCount = lifeSpan
            };

            if (parameters?.Any() == true)
            {
                context.Parameters = new Google.Protobuf.WellKnownTypes.Struct();

                foreach (var parameter in parameters)
                {
                    var value = new Google.Protobuf.WellKnownTypes.Value
                    {
                        StringValue = parameter.Value
                    };

                    context.Parameters.Fields.Add(parameter.Key, value);
                }
            }

            return context;
        }
    }
}
