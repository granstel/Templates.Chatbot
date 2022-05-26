using AutoMapper;
using GranSteL.Chatbot.Models;
using Telegram.Bot.Types;

namespace GranSteL.Chatbot.Messengers.Telegram
{
    public class TelegramMapping : Profile
    {
        public TelegramMapping()
        {
            CreateMap<Update, Request>()
            .ForMember(d => d.ChatHash, m => m.MapFrom((s, d) => (s.Message?.Chat?.Id).GetValueOrDefault()))
            .ForMember(d => d.UserHash, m => m.MapFrom((s, d) => (s.Message?.From?.Id).GetValueOrDefault()))
            .ForMember(d => d.SessionId, m => m.MapFrom((s, d) => (s.Message?.From?.Id).GetValueOrDefault()))
            .ForMember(d => d.Text, m => m.MapFrom((s, d) => s.Message?.Text))
            .ForMember(d => d.Source, m => m.MapFrom(s => "Telegram"))
            .ForMember(d => d.Appeal, m => m.MapFrom(s => Appeal.NoOfficial))
            .ForMember(d => d.HasScreen, m => m.MapFrom(s => true))
            .ForMember(d => d.Language, m => m.Ignore())
            .ForMember(d => d.NewSession, m => m.Ignore());
        }
    }
}
