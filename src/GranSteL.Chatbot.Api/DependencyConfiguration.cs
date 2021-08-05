using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GranSteL.Chatbot.Api.DependencyModules;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GranSteL.Chatbot.Api
{
    internal static class DependencyConfiguration
    {
        internal static void Configure(IServiceCollection services, IConfiguration appConfiguration)
        {
            var configuration = appConfiguration.GetSection($"{nameof(AppConfiguration)}").Get<AppConfiguration>();
            
            services.AddSingleton(configuration);
            services.AddSingleton(configuration.HttpLog);
            services.AddSingleton(configuration.Redis);
            services.AddSingleton(configuration.Dialogflow);

            services.AddInternalServices();
            services.AddExternalServices();

            var names = GetAssembliesNames();
            services.AddMapping(names);
        }


        public static ICollection<string> GetAssembliesNames()
        {
            var callingAssemble = Assembly.GetCallingAssembly();

            var names = callingAssemble.GetCustomAttributes<ApplicationPartAttribute>()
                .Where(a => a.AssemblyName.Contains("GranSteL.Chatbot", StringComparison.InvariantCultureIgnoreCase))
                .Select(a => a.AssemblyName).ToList();

            return names;
        }
    }
}
