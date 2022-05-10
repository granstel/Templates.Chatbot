using System;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using GranSteL.Chatbot.Services.Configuration;
using GranSteL.Helpers.Redis;
using Grpc.Auth;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GranSteL.Chatbot.Api.DependencyModules
{
    internal static class ExternalServicesRegistration
    {
        internal static void AddExternalServices(this IServiceCollection services)
        {
            services.AddSingleton<SessionsClient>(RegisterDialogflowSessionsClient);

            services.AddSingleton<IDatabase>(RegisterRedisClient);

            services.AddSingleton<IRedisCacheService>(RegisterCacheService);
        }

        private static SessionsClient RegisterDialogflowSessionsClient(IServiceProvider provider)
        {
            var configuration = provider.GetService<DialogflowConfiguration>();

            var credential = GoogleCredential.FromFile(configuration.JsonPath).CreateScoped(SessionsClient.DefaultScopes);

            var clientBuilder = new SessionsClientBuilder
            {
                ChannelCredentials = credential.ToChannelCredentials()
            };

            var client = clientBuilder.Build();

            return client;
        }

        private static IDatabase RegisterRedisClient(IServiceProvider provider)
        {
            var configuration = provider.GetService<RedisConfiguration>();

            var redisClient = ConnectionMultiplexer.Connect(configuration.ConnectionString);

            var dataBase = redisClient.GetDatabase();

            return dataBase;
        }

        private static RedisCacheService RegisterCacheService(IServiceProvider provider)
        {
            var configuration = provider.GetService<RedisConfiguration>();

            var db = provider.GetService<IDatabase>();

            var service = new RedisCacheService(db, configuration.KeyPrefix);

            return service;
        }
    }
}
