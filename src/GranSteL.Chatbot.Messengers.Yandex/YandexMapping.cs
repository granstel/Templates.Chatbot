using System;
using AutoMapper;
using GranSteL.Chatbot.Models;
using Yandex.Dialogs.Models;
using Yandex.Dialogs.Models.Buttons;
using Yandex.Dialogs.Models.Input;
using InternalModels = GranSteL.Chatbot.Models;
using YandexModels = Yandex.Dialogs.Models;

namespace GranSteL.Chatbot.Messengers.Yandex
{
    /// <summary>
    /// Probably, registered at MappingRegistration of "Services" project
    /// </summary>
    public class YandexMapping : Profile
    {
        public YandexMapping()
        {
            CreateMap<InputModel, InternalModels.Request>()
                .ForMember(d => d.ChatHash, m => m.MapFrom((s, d) => s.Session?.SkillId))
                .ForMember(d => d.UserHash, m => m.MapFrom((s, d) => s.Session?.UserId))
                .ForMember(d => d.Text, m => m.MapFrom((s, d) => s.Request?.OriginalUtterance))
                .ForMember(d => d.SessionId, m => m.MapFrom((s, d) => s.Session?.SessionId))
                .ForMember(d => d.NewSession, m => m.MapFrom((s, d) => s.Session?.New))
                .ForMember(d => d.Language, m => m.MapFrom((s, d) => s.Meta?.Locale))
                .ForMember(d => d.Source, m => m.MapFrom(s => "Yandex"))
                .ForMember(d => d.Appeal, m => m.MapFrom(s => Appeal.NoOfficial))
                .ForMember(d => d.HasScreen, m => m.MapFrom((s, d) => s?.Meta?.Interfaces?.Screen != null));

            CreateMap<InternalModels.Response, OutputModel>()
                .ForMember(d => d.Response, m => m.MapFrom(s => s))
                .ForMember(d => d.Session, m => m.MapFrom(s => s))
                .ForMember(d => d.Version, m => m.Ignore())
                .ForMember(d => d.StartAccountLinking, m => m.Ignore())
                .ForMember(d => d.UserStateUpdate, m => m.Ignore())
                .ForMember(d => d.SessionState, m => m.Ignore())
                .ForMember(d => d.ApplicationState, m => m.Ignore())
                .ForMember(d => d.Analytics, m => m.Ignore());

            CreateMap<InternalModels.Response, YandexModels.Response>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Text.Replace(Environment.NewLine, "\n")))
                .ForMember(d => d.Tts, m => m.MapFrom(s => s.AlternativeText.Replace(Environment.NewLine, "\n")))
                .ForMember(d => d.EndSession, m => m.MapFrom(s => s.Finished))
                .ForMember(d => d.Buttons, m => m.MapFrom(s => s.Buttons))
                .ForMember(d => d.Card, m => m.Ignore())
                .ForMember(d => d.Directives, m => m.Ignore());

            CreateMap<InternalModels.Response, Session>()
                .ForMember(d => d.UserId, m => m.MapFrom(s => s.UserHash))
                .ForMember(d => d.MessageId, m => m.Ignore())
                .ForMember(d => d.SessionId, m => m.Ignore())
                .ForMember(d => d.Application, m => m.Ignore())
                .ForMember(d => d.User, m => m.Ignore());

            CreateMap<InputModel, OutputModel>()
                .ForMember(d => d.Session, m => m.MapFrom(s => s.Session))
                .ForMember(d => d.Version, m => m.MapFrom(s => s.Version))
                .ForMember(d => d.Response, m => m.Ignore())
                .ForMember(d => d.StartAccountLinking, m => m.Ignore())
                .ForMember(d => d.UserStateUpdate, m => m.Ignore())
                .ForMember(d => d.SessionState, m => m.Ignore())
                .ForMember(d => d.ApplicationState, m => m.Ignore())
                .ForMember(d => d.Analytics, m => m.Ignore());

            CreateMap<InternalModels.Button, ResponseButton>()
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.Url, m => m.MapFrom(s => !string.IsNullOrEmpty(s.Url) ? s.Url : null))
                .ForMember(d => d.Hide, m => m.MapFrom(s => s.IsQuickReply))
                .ForMember(d => d.Payload, m => m.Ignore());
        }
    }
}
