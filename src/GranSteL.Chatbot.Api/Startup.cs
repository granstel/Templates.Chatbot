using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GranSteL.Chatbot.Api.Middleware;
using GranSteL.Chatbot.Services.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GranSteL.Chatbot.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        private IContainer _applicationContainer;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // ReSharper disable once UnusedMember.Global
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddNewtonsoftJson();

            _applicationContainer = DependencyConfiguration.Configure(services, _configuration);

            return new AutofacServiceProvider(_applicationContainer);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppConfiguration configuration)
        {
            app.UseMiddleware<ExceptionsMiddleware>();

            if (configuration.HttpLog.Enabled)
            {
                app.UseMiddleware<HttpLogMiddleware>();
            }

            app.UseRouting();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}
