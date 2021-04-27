using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NLog;
using Sber.SmartApp.Models;
using Sber.SmartApp.Models.Constants;
using InternalModels = GranSteL.Chatbot.Models;
using SberModels = Sber.SmartApp.Models;

namespace GranSteL.Chatbot.Messengers.Sber
{
    /// <summary>
    /// Probably, registered at MappingModule of "Services" project
    /// </summary>
    public class SberProfile : Profile
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public SberProfile()
        {
            CreateMap<Request, InternalModels.Request>()
                .ForMember(d => d.ChatHash, m => m.MapFrom((s, d) => s?.Payload?.AppInfo?.ProjectId.ToString()))
                .ForMember(d => d.UserHash, m => m.MapFrom((s, d) => s?.Uuid?.Sub ?? s?.Uuid?.UserId))
                .ForMember(d => d.Text, m => m.MapFrom((s, d) =>
                {
                    const string replacedObsceneWord = "кое-что";
                    const string stars = "***";

                    var asrNormalizedMessage = s?.Payload?.Message?.AsrNormalizedMessage;

                    try
                    {
                        if (asrNormalizedMessage?.Contains(stars) == true)
                        {
                            return asrNormalizedMessage.Replace(stars, replacedObsceneWord);
                        }

                        var obsceneIndex = s?.Payload?.Annotations?.CensorData?.Classes?.ToList().IndexOf("obscene") ?? 0;

                        if (s?.Payload?.Annotations?.CensorData?.Probas[obsceneIndex] == 1.0)
                        {
                            return replacedObsceneWord;
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }

                    return s?.Payload?.Message?.OriginalText;
                }))
                .ForMember(d => d.SessionId, m => m.MapFrom((s, d) => s?.SessionId))
                .ForMember(d => d.NewSession, m => m.MapFrom((s, d) => s?.Payload?.NewSession))
                .ForMember(d => d.Language, m => m.Ignore())
                .ForMember(d => d.HasScreen, m => m.MapFrom((s, d) => s?.Payload?.Device?.Capabilities?.Screen?.Available ?? false))
                .ForMember(d => d.Source, m => m.MapFrom(s => InternalModels.Source.Sber))
                .ForMember(d => d.Appeal, m => m.MapFrom((s, d) =>
                {
                    var appeal = InternalModels.Appeal.NoOfficial;

                    if (string.Equals(s.Payload?.Character?.Appeal, "official"))
                    {
                        appeal = InternalModels.Appeal.Official;
                    }

                    return appeal;
                }));

            CreateMap<InternalModels.Response, Response>()
                .ForMember(d => d.Payload, m => m.MapFrom(s => s))
                ;

            CreateMap<InternalModels.Response, ResponsePayload>()
                .ForMember(d => d.PronounceText, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.PronounceTextType, m => m.MapFrom(s => PronounceTextTypeValues.Text))
                .ForMember(d => d.AutoListening, m => m.MapFrom(s => !s.Finished))
                .ForMember(d => d.Finished, m => m.MapFrom(s => s.Finished))
                .ForMember(d => d.Items, m => m.MapFrom(s => s))
                .ForMember(d => d.Suggestions, m => m.MapFrom(s => s.Buttons.Where(b => b.IsQuickReply)));

            CreateMap<string, Emotion>()
                .ForMember(d => d.EmotionId, m => m.MapFrom(s => s));

            CreateMap<InternalModels.Response, PayloadItem[]>().ConvertUsing(MapResponseToItem);

            CreateMap<IEnumerable<InternalModels.Button>, Suggestion>().ConvertUsing(MapButtonsToSuggestion);


            CreateMap<InternalModels.Button, Button>()
                .ForMember(d => d.Title, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.Action, m => m.MapFrom(s => s))
                ;

            CreateMap<InternalModels.Button, SberModels.Action>()
                .ForMember(d => d.Text, m => m.MapFrom(s => s.Text))
                .ForMember(d => d.DeepLink, m => m.MapFrom(s => s.Url))
                .ForMember(d => d.Type, m => m.MapFrom((s, d) =>
                {
                    if (!string.IsNullOrEmpty(s.Url))
                    {
                        return ActionTypeValues.DeepLink;
                    }

                    return ActionTypeValues.Text;
                }));

            CreateMap<Request, Response>()
                .ForMember(d => d.MessageName, m => m.MapFrom(s => MessageNameValues.AnswerToUser))
                .ForMember(d => d.SessionId, m => m.MapFrom(s => s.SessionId))
                .ForMember(d => d.MessageId, m => m.MapFrom(s => s.MessageId))
                .ForMember(d => d.Uuid, m => m.MapFrom(s => s.Uuid))
                .ForMember(d => d.Payload, m => m.MapFrom(s => s.Payload))
                ;

            CreateMap<RequestPayload, ResponsePayload>()
                .ForMember(d => d.Device, m => m.MapFrom(s => s.Device))
                .ForMember(d => d.ProjectName, m => m.Ignore())
                .ForMember(d => d.Intent, m => m.Ignore())
                ;
        }

        private PayloadItem[] MapResponseToItem(InternalModels.Response source, PayloadItem[] destinations, ResolutionContext context)
        {
            var itemWithBubble = new PayloadItem
            {
                Bubble = { Text = source.Text }
            };

            var buttons = source.Buttons?.Where(b => !b.IsQuickReply).ToList();

            var cardItems = buttons?.Select(b =>
            {
                var cardItem = new CardCell
                {
                    Type = CellTypeValues.GreetingGridItem,
                    TopText = new CardCellText
                    {
                        Type = CellTypeValues.TextCellView,
                        Text = string.Empty,
                        Typeface = TypefaceValues.Caption,
                        TextColor = TextColorValues.Default
                    },
                    BottomText = new CardCellText
                    {
                        Type = CellTypeValues.LeftRightCellView,
                        Text = b.Text,
                        Typeface = TypefaceValues.Button1,
                        TextColor = TextColorValues.Default,
                        MaxLines = 2,
                        Margins = new Margins
                        {
                            Top = IndentValues.X0
                        }
                    },
                    Paddings = new Paddings
                    {
                        Top = IndentValues.X0,
                        Left = IndentValues.X6,
                        Right = IndentValues.X6,
                        Bottom = IndentValues.X16
                    }
                };

                var action = context.Mapper.Map<SberModels.Action>(b);

                cardItem.Actions = new[] { action };

                return cardItem;
            }).ToArray();

            var card = new Card
            {
                Type = CardTypeValues.GridCard,
                Items = cardItems,
                Columns = 2,
                ItemWidth = ItemWidthValues.Resizable
            };

            var itemWithCard = new PayloadItem
            {
                Card = card
            };

            return new[] { itemWithBubble, itemWithCard };
        }

        private Suggestion MapButtonsToSuggestion(IEnumerable<InternalModels.Button> source, Suggestion destination, ResolutionContext context)
        {
            var buttons = context.Mapper.Map<Button[]>(source);

            var suggest = new Suggestion
            {
                Buttons = buttons
            };

            return suggest;
        }
    }
}
