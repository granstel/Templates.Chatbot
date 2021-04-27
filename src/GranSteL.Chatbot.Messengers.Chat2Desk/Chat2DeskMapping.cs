using AutoMapper;
using GranSteL.Chatbot.Messengers.Chat2Desk.Models;
using GranSteL.Chatbot.Models;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class Chat2DeskMapping : Profile
    {
        public Chat2DeskMapping()
        {
            CreateMap<Message, Request>()
                .ForMember(d => d.ChatHash, m => m.MapFrom(s => s.ClientId))
                .ForMember(d => d.UserHash, m => m.MapFrom(s => s.ClientId))
                .ForMember(d => d.SessionId, m => m.MapFrom(s => s.ClientId))
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.Source, m => m.MapFrom(s => Source.Chat2Desk))
                .ForMember(d => d.Appeal, m => m.MapFrom(s => Appeal.NoOfficial))
                .ForMember(d => d.Language, m => m.Ignore())
                .ForMember(d => d.NewSession, m => m.Ignore());

            CreateMap<Response, Message>()
                .ForMember(d => d.ClientId, m => m.MapFrom(s => s.ChatHash))
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.Coordinates, m => m.Ignore())
                .ForMember(d => d.Created, m => m.Ignore())
                .ForMember(d => d.MessageId, m => m.Ignore())
                .ForMember(d => d.Read, m => m.Ignore())
                .ForMember(d => d.RecipientStatus, m => m.Ignore())
                .ForMember(d => d.Transport, m => m.Ignore())
                .ForMember(d => d.Type, m => m.Ignore())
                .ForMember(d => d.VbBusId, m => m.Ignore());
        }
    }
}
