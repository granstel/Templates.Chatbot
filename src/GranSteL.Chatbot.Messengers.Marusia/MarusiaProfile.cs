using System;
using AutoMapper;
using FillInTheTextBot.Models;
using MailRu.Marusia.Models.Buttons;
using MailRu.Marusia.Models.Input;
using MarusiaModels = MailRu.Marusia.Models;

namespace FillInTheTextBot.Messengers.Marusia
{
    /// <summary>
    /// Probably, registered at MappingModule of "Services" project
    /// </summary>
    public class MarusiaProfile : Profile
    {
        public MarusiaProfile()
        {
            CreateMap<InputModel, Request>()
                .ForMember(d => d.ChatHash, m => m.ResolveUsing(s => s.Session?.SkillId))
                .ForMember(d => d.UserHash, m => m.ResolveUsing(s => s.Session?.UserId))
                .ForMember(d => d.Text, m => m.ResolveUsing(s => s.Request?.OriginalUtterance))
                .ForMember(d => d.SessionId, m => m.ResolveUsing(s => s.Session?.SessionId))
                .ForMember(d => d.NewSession, m => m.ResolveUsing(s => s.Session?.New))
                .ForMember(d => d.Language, m => m.ResolveUsing(s => s.Meta?.Locale))
                .ForMember(d => d.HasScreen, m => m.ResolveUsing(s => string.Equals(s?.Session?.Application?.ApplicationType, MarusiaModels.ApplicationTypes.Mobile)))
                .ForMember(d => d.ClientId, m => m.ResolveUsing(s => s?.Meta?.ClientId))
                .ForMember(d => d.Source, m => m.UseValue(Source.Marusia))
                .ForMember(d => d.Appeal, m => m.UseValue(Appeal.NoOfficial))
                .ForMember(d => d.RequiredContexts, m => m.Ignore())
                .ForMember(d => d.IsOldUser, m => m.Ignore())
                .ForMember(d => d.NextTextIndex, m => m.Ignore())
                .ForMember(d => d.ScopeKey, m => m.Ignore());

            CreateMap<Response, MarusiaModels.OutputModel>()
                .ForMember(d => d.Response, m => m.MapFrom(s => s))
                .ForMember(d => d.Session, m => m.MapFrom(s => s))
                .ForMember(d => d.Version, m => m.Ignore())
                .ForMember(d => d.UserStateUpdate, m => m.Ignore())
                .ForMember(d => d.SessionState, m => m.Ignore());

            CreateMap<Response, MarusiaModels.Response>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Text.Replace(Environment.NewLine, "\n")))
                .ForMember(d => d.Tts, m => m.MapFrom(s => s.AlternativeText.Replace(Environment.NewLine, "\n")))
                .ForMember(d => d.EndSession, m => m.MapFrom(s => s.Finished))
                .ForMember(d => d.Buttons, m => m.MapFrom(s => s.Buttons))
                .ForMember(d => d.Card, m => m.Ignore());

            CreateMap<Response, MarusiaModels.Session>()
                .ForMember(d => d.UserId, m => m.MapFrom(s => s.UserHash))
                .ForMember(d => d.MessageId, m => m.Ignore())
                .ForMember(d => d.SessionId, m => m.Ignore())
                .ForMember(d => d.Application, m => m.Ignore())
                .ForMember(d => d.User, m => m.Ignore());

            CreateMap<InputModel, MarusiaModels.OutputModel>()
                .ForMember(d => d.Session, m => m.MapFrom(s => s.Session))
                .ForMember(d => d.Version, m => m.MapFrom(s => s.Version))
                .ForMember(d => d.Response, m => m.Ignore())
                .ForMember(d => d.UserStateUpdate, m => m.Ignore())
                .ForMember(d => d.SessionState, m => m.Ignore());

            CreateMap<Models.Button, ResponseButton>()
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.Url, m => m.MapFrom(s => !string.IsNullOrEmpty(s.Url) ? s.Url : null))
                .ForMember(d => d.Hide, m => m.MapFrom(s => s.IsQuickReply))
                .ForMember(d => d.Payload, m => m.Ignore());
        }
    }
}
