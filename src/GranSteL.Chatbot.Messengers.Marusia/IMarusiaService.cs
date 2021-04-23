using FillInTheTextBot.Services;
using MailRu.Marusia.Models;
using MailRu.Marusia.Models.Input;

namespace FillInTheTextBot.Messengers.Marusia
{
    public interface IMarusiaService : IMessengerService<InputModel, OutputModel>
    {
    }
}