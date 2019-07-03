using System;

namespace GranSteL.Chatbot.Services.Configuration
{
    public abstract class Configuration
    {
        public string ExpandVariable(string variableName)
        {
            return Environment.ExpandEnvironmentVariables(variableName ?? string.Empty);
        }
    }
}
