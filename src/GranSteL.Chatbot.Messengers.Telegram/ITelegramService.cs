using System.Threading.Tasks;
using GranSteL.Chatbot.Services;
using Telegram.Bot.Types;

namespace GranSteL.Chatbot.Messengers.Telegram
{
    public interface ITelegramService : IMessengerService<Update, string>
    {
        Task<bool> TestApiAsync();

        Task<User> GetMeAsync();
    }
}