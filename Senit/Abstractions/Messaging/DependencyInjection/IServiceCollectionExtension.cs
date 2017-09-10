﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Enrichers.GlobalExecutionId;
using RawRabbit.Enrichers.HttpContext;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;

namespace Senit.Abstractions.Messaging.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, RawRabbitConfiguration configuration)
        {
            services.AddRawRabbit(new RawRabbitOptions
            {
                ClientConfiguration = configuration,
                Plugins = p => p
                    .UseGlobalExecutionId()
                    .UseHttpContext()
                    .UseMessageContext(ctx => new MessageContext
                    {
                        Source = ctx.GetHttpContext().Request.GetDisplayUrl(),
                        ExecutionId = ctx.GetGlobalExecutionId()
                    })
                    .UseAttributeRouting()
                    .UseStateMachine()
            });

            services.AddTransient<BusClientWrapper, BusClientWrapper>();

            return services;
        }
    }
}
