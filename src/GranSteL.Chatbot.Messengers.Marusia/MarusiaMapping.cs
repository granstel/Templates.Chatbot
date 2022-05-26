using System;
using System.Collections.Generic;
using GranSteL.Chatbot.Models;
using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Buttons;
using MailRu.Marusia.Models.Input;
using MarusiaModels = MailRu.Marusia.Models;

namespace GranSteL.Chatbot.Messengers.Marusia
{
    public static class MarusiaMapping
    {
        public static Models.Request ToRequest(this InputModel source)
        {
            if (source == null) return null;

            var destinaton = new Models.Request();

            destinaton.ChatHash = source.Session?.SkillId;
            destinaton.UserHash = source.Session?.UserId;
            destinaton.Text = source.Request?.OriginalUtterance;
            destinaton.SessionId = source.Session?.SessionId;
            destinaton.NewSession = source.Session?.New;
            destinaton.Language = source.Meta?.Locale;
            destinaton.HasScreen = string.Equals(source?.Session?.Application?.ApplicationType, MarusiaModels.ApplicationTypes.Mobile);
            destinaton.Source = "Marusia";
            destinaton.Appeal = Appeal.NoOfficial;

            return destinaton;
        }

        public static OutputModel ToOutput(this Models.Response source)
        {
            if (source == null) return null;

            var destination = new OutputModel();

            destination.Response = source.ToResponse();
            destination.Session = source.ToSession();

            return destination;
        }

        public static MarusiaModels.Response ToResponse(this Models.Response source)
        {
            if (source == null) return null;

            var destination = new MarusiaModels.Response();

            destination.Text = source.Text?.Replace(Environment.NewLine, "\n");
            destination.Tts = source.AlternativeText?.Replace(Environment.NewLine, "\n");
            destination.EndSession = source.Finished;
            destination.Buttons = source.Buttons?.ToResponseButtons();

            return destination;
        }

        public static Session ToSession(this Models.Response source)
        {
            if (source == null) return null;

            var destination = new Session
            {
                UserId = source.UserHash
            };

            return destination;
        }

        public static ResponseButton[] ToResponseButtons(this ICollection<Models.Button> source)
        {
            if (source == null) return null;

            var responseButtons = new List<ResponseButton>();

            foreach (var button in source)
            {
                var responseButton = new ResponseButton();

                responseButton.Title = button?.Text;
                responseButton.Url = !string.IsNullOrEmpty(button?.Url) ? button?.Url : null;
                responseButton.Hide = button.IsQuickReply;

                responseButtons.Add(responseButton);
            }

            return responseButtons.ToArray();
        }

        public static OutputModel FillOutput(this InputModel source, OutputModel destination)
        {
            if (source == null) return null;
            if (destination == null) return null;

            destination.Session = source.Session;
            destination.Version = source.Version;

            return destination;
        }
    }
}
