using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GranSteL.Chatbot.Api.DependencyModules;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GranSteL.Chatbot.Api
{
    internal static class DependencyConfiguration
    {
        internal static IContainer Configure(IServiceCollection services, IConfiguration appConfiguration)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);

            var configuration = appConfiguration.GetSection($"{nameof(AppConfiguration)}").Get<AppConfiguration>();
            
            containerBuilder.RegisterInstance(configuration).SingleInstance();
            containerBuilder.RegisterInstance(configuration.HttpLog).SingleInstance();
            containerBuilder.RegisterInstance(configuration.Redis).SingleInstance();
            containerBuilder.RegisterInstance(configuration.Dialogflow).SingleInstance();

            containerBuilder.RegisterModule<InternalServicesModule>();
            containerBuilder.RegisterModule<ExternalServicesModule>();

            var names = GetAssembliesNames();
            containerBuilder.RegisterModule(new MappingModule(names));
            RegisterFromMessengersAssemblies(containerBuilder, names);

            return containerBuilder.Build();
        }

        private static void RegisterFromMessengersAssemblies(ContainerBuilder containerBuilder, string[] names)
        {
            foreach (var name in names)
            {
                var assembly = Assembly.Load(name);

                containerBuilder.RegisterAssemblyModules(assembly);
            }
        }

        private static string[] GetAssembliesNames()
        {
            var result = new List<string>();

            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "GranSteL.Chatbot*.dll");

            foreach (var file in files)
            {
                var info = new FileInfo(file);

                var name = info.Name.Replace(info.Extension, string.Empty);

                if (name.Equals(AppDomain.CurrentDomain.FriendlyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                result.Add(name);
            }

            return result.ToArray();
        }
    }
}
