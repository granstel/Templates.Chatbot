using GranSteL.Chatbot.Messengers.Chat2Desk;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Chat2DeskStartup))]
namespace GranSteL.Chatbot.Messengers.Chat2Desk
{
    public class Chat2DeskStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddConfiguration<Chat2DeskConfiguration>("appsettings.Chat2Desk.json");

                services.AddTransient<IChat2DeskService, Chat2DeskService>();
                services.AddTransient<IChat2DeskClient, Chat2DeskClient>();
            });
        }
    }
}
