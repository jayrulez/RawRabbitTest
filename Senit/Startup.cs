using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.HttpContext;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using Senit.Abstractions.Messaging;
using Senit.Abstractions.Messaging.DependencyInjection;
using Senit.Messaging.Handlers;
using Senit.Messaging.Messages;
using System;
using System.Linq;

namespace Senit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddMessaging(GetRawRabbitConfiguration());

            services.AddTransient<TestRequestMessageHandler, TestRequestMessageHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.AddRpcHandler<TestRequestMessage, TestResponseMessage, TestRequestMessageHandler>();

            app.UseMvc();
        }

        private RawRabbitConfiguration GetRawRabbitConfiguration()
        {
            var section = Configuration.GetSection("RawRabbit");

            if (!section.GetChildren().Any())
            {
                throw new ArgumentException($"Unable to get configuration section 'RawRabbit'. Make sure it exists in the provided configuration");
            }

            return section.Get<RawRabbitConfiguration>();
        }
    }
}
