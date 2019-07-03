using System.Threading.Tasks;

namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public interface IChat2DeskClient
    {
        Task<bool> SetWebhookAsync(string url);

        Task<bool> DeleteWebHookAsync();

        Task<bool> SendTextMessageAsync(long chatId, string text);
    }
}