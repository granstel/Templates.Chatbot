using System.Linq;
using GranSteL.Chatbot.Api.Extensions;
using GranSteL.Chatbot.Api.Middleware;
using GranSteL.Chatbot.Services;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GranSteL.Chatbot.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            InternalLoggerFactory.Factory = loggerFactory;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddNewtonsoftJson();

            services.AddHttpLogging(o =>
            {
                o.LoggingFields = HttpLoggingFields.All;
            });

            DependencyConfiguration.Configure(services, _configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppConfiguration configuration)
        {
            app.UseMiddleware<ExceptionsMiddleware>();

            app.UseRouting();

            if (configuration.HttpLog.Enabled)
            {
                app.UseWhen(context => configuration.HttpLog.IncludeEndpoints.Any(context.ContainsEndpoint), a =>
                {
                    a.UseHttpLogging();
                });
            }

            app.UseEndpoints(e => e.MapControllers());
        }
    }
}
