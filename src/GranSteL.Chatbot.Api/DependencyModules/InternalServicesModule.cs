﻿using Autofac;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Serialization;
using GranSteL.Helpers.Redis;

namespace GranSteL.Chatbot.Api.DependencyModules
{
    public class InternalServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConversationService>().As<IConversationService>();
            builder.RegisterType<QnaService>().As<IQnaService>();
            builder.RegisterType<DialogflowService>().As<IDialogflowService>();
            builder.RegisterType<RedisCacheService>().As<IRedisCacheService>();
            builder.RegisterType<CustomJsonSerializer>().AsSelf();
        }
    }
}
