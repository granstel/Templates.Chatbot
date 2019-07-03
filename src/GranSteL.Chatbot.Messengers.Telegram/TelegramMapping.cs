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
            .ForMember(d => d.ChatHash, m => m.ResolveUsing(s => (s.Message?.Chat?.Id).GetValueOrDefault()))
            .ForMember(d => d.UserHash, m => m.ResolveUsing(s => (s.Message?.From?.Id).GetValueOrDefault()))
            .ForMember(d => d.RequestText, m => m.ResolveUsing(s => s.Message?.Text))
            .ForMember(d => d.Source, m => m.UseValue(Source.Telegram));
        }
    }
}
