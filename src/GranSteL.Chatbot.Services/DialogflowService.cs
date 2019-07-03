using System;
using System.Linq;
using ApiAiSDK;

namespace GranSteL.Chatbot.Services
{
    public class DialogflowService : IDialogflowService
    {
        private readonly IApiAi _dialogflowClient;

        public DialogflowService(IApiAi dialogflowClient)
        {
            _dialogflowClient = dialogflowClient;
        }

        public string GetGreeting(string name)
        {
            throw new NotImplementedException();
        }

        public string GetCity(string requestMessageText)
        {
            var response = _dialogflowClient.TextRequest(requestMessageText);

            var cityName = response.Result.Parameters?
                .Where(p => string.Equals("geo-city", p.Key))
                .Select(p => p.Value as string)
                .FirstOrDefault();


            return cityName ?? requestMessageText;
        }

        public DateTime? GetDate(string requestMessageText)
        {
            var response = _dialogflowClient.TextRequest(requestMessageText);

            var dateString = response.Result.Parameters?
                .Where(p => string.Equals("date", p.Key))
                .Select(p => p.Value as string)
                .FirstOrDefault();

            if (DateTime.TryParse(dateString, out var date))
            {
                return date;
            }

            return null;
        }
    }
}
