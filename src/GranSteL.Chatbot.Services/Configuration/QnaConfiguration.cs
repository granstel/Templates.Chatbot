namespace GranSteL.Chatbot.Services.Configuration
{
    public class QnaConfiguration : Configuration
    {
        private string _urlFormat;
        public virtual string UrlFormat
        {
            get => _urlFormat;
            set => _urlFormat = ExpandVariable(value);
        }

        private string _token;
        public virtual string Token
        {
            get => _token;
            set => _token = ExpandVariable(value);
        }

        public virtual string KnowledgeBase { get; set; }
    }
}
