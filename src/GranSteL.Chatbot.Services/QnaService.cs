using System;
using System.Linq;
using System.Threading.Tasks;
using GranSteL.Chatbot.Models.Qna;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Chatbot.Services.Extensions;
using NLog;

namespace GranSteL.Chatbot.Services
{
    public class QnaService : IQnaService
    {
        private readonly IQnaClient _client;
        private readonly QnaConfiguration _configuration;

        private readonly Logger _log = LogManager.GetLogger(nameof(QnaService));

        public QnaService(IQnaClient client, QnaConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<string> GetAnswerAsync(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                return "Не задан вопрос";
            }

            var knowledgeBase = _configuration.KnowledgeBase;

            Response qnaResponse;

            if (string.IsNullOrEmpty(knowledgeBase))
            {
                qnaResponse = ResponseWithError("Не удалось найти базу знаний");
            }
            else
            {
                try
                {
                    qnaResponse = await _client.GetAnswerAsync(knowledgeBase, question);
                }
                catch (Exception e)
                {
                    _log.Warn(e, $"Не удалось получить ответ на вопрос \"{question}\"");

                    qnaResponse = ResponseWithError("Не удалось получить ответ");
                }
            }

            var answer = ExtractAnswer(qnaResponse);

            _log.Info($"Question = {question}, answer = {answer}");

            return answer;
        }

        private string ExtractAnswer(Response qnaResponse)
        {
            _log.Info(qnaResponse.Serialize());

            string answer;

            if (qnaResponse != null && qnaResponse.Error == null)
            {
                var maxScore = qnaResponse.Answers.Max(a => a.Score);

                answer = qnaResponse.Answers.FirstOrDefault(a => Math.Abs(maxScore - a.Score) < 1)?.Answer.Sanitize();
            }
            else
            {
                _log.Warn($"Возникла ошибка при получении ответа: {qnaResponse?.Error.Message} ({qnaResponse?.Error.Code})");

                answer = $"Возникла ошибка: {qnaResponse?.Error.Message ?? "не получен ответ"}";
            }

            return answer;
        }

        private Response ResponseWithError(string message)
        {
            return new Response
            {
                Error = new Error { Code = "custom", Message = message }
            };
        }
    }
}
