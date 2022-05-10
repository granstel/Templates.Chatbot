using GranSteL.Chatbot.Messengers.Sber;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(SberStartup))]
namespace GranSteL.Chatbot.Messengers.Sber
{
    public class SberStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddConfiguration<SberConfiguration>("appsettings.Sber.json");

                services.AddTransient<ISberService, SberService>();
            });
        }
    }
}
