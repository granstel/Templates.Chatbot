using AutoMapper;
using GranSteL.Chatbot.Models.Internal;
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
            .ForMember(d => d.Source, m => m.MapFrom(s => Source.Telegram))
            .ForMember(d => d.Language, m => m.Ignore())
            .ForMember(d => d.NewSession, m => m.Ignore());
        }
    }
}
