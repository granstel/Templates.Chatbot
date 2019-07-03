using Microsoft.AspNetCore.Mvc;

namespace GranSteL.Chatbot.Messengers.Extensions
{
    public static class ControllerExtension
    {
        public static string GetWebHookUrl(this Controller controller)
        {
            var request = controller.Request;

            var pathBase = request.PathBase.Value;
            var pathSegment = request.Path.Value;

            var url = $"https://{request.Host}{pathBase}{pathSegment}";

            return url;
        }
    }
}
