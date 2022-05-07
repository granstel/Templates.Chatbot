using System;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GranSteL.Chatbot.Messengers
{
    public static class MessengerConfigurationRegistration
    {
        public static void AddConfiguration<T>(this IServiceCollection services, string fileName) where T : MessengerConfiguration
        {
            services.AddSingleton(context =>
            {
                const string extension = ".json";

                if (fileName.IndexOf(extension, StringComparison.InvariantCultureIgnoreCase) < 0)
                {
                    fileName = $"{fileName}{extension}";
                }

                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile(fileName, true, false);

                var configurationRoot = configurationBuilder.Build();

                var configuration = configurationRoot.Get<T>();

                return configuration;
            });
        }
    }
}
