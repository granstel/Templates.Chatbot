namespace GranSteL.Chatbot.Services.Configuration
{
    public class AppConfiguration
    {
        public QnaConfiguration Qna { get; set; }

        public DialogflowConfiguration Dialogflow { get; set; }

        public RedisConfiguration Redis { get; set; }
    }
}
