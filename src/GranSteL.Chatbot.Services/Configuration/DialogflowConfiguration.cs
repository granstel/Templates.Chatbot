namespace GranSteL.Chatbot.Services.Configuration
{
    public class DialogflowConfiguration : Configuration
    {
        private string _token;
        public virtual string Token
        {
            get => _token;
            set => _token = ExpandVariable(value);
        }

        private string _projectId;
        public virtual string ProjectId
        {
            get => _projectId;
            set => _projectId = ExpandVariable(value);
        }

        private string _languageCode;
        public virtual string LanguageCode
        {
            get => _languageCode;
            set => _languageCode = ExpandVariable(value);
        }

        private string _jsonPath;
        public virtual string JsonPath
        {
            get => _jsonPath;
            set => _jsonPath = ExpandVariable(value);
        }
    }
}
