using System;
using Autofac;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.Extensions.Configuration;

namespace GranSteL.Chatbot.Messengers
{
    public abstract class MessengerModule : Module
    {
        private const string Extension = ".json";

        protected T GetConfigurationFromFile<T>(string fileName) where T : MessengerConfiguration
        {
            if (fileName.IndexOf(Extension, StringComparison.InvariantCultureIgnoreCase) < 0)
            {
                fileName = $"{fileName}{Extension}";
            }

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(fileName, true, false);

            var configurationRoot = configurationBuilder.Build();

            var configuration = configurationRoot.Get<T>();

            return configuration;
        }
    }
}
