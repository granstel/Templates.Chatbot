using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace GranSteL.Chatbot.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);

            var hostingStartupAssemblies = builder.GetSetting(WebHostDefaults.HostingStartupAssembliesKey) ?? string.Empty;
            var hostingStartupAssembliesList = hostingStartupAssemblies.Split(';');

            var names = DependencyConfiguration.GetAssembliesNames();
            var fullList = hostingStartupAssembliesList.Concat(names).Distinct().ToList();
            var concatenatedNames = string.Join(';', fullList);

            var host = builder
                .UseSetting(WebHostDefaults.HostingStartupAssembliesKey, concatenatedNames)
                .UseStartup<Startup>()
                .UseNLog()
                .Build();

            return host;
        }
    }
}
