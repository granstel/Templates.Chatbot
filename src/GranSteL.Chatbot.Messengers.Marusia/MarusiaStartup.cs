using GranSteL.Chatbot.Messengers.Marusia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(MarusiaStartup))]
namespace GranSteL.Chatbot.Messengers.Marusia
{
    public class MarusiaStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddConfiguration<MarusiaConfiguration>("appsettings.Marusia.json");

                services.AddTransient<IMarusiaService, MarusiaService>();
            });
        }
    }
}
