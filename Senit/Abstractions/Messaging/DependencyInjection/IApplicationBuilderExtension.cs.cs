using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Senit.Abstractions.Messaging.DependencyInjection
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder AddMessageHandler<TMessage, TMessageHandler>(this IApplicationBuilder app)
            where TMessageHandler : IMessageHandler<TMessage>
            where TMessage : IMessage
        {
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger(nameof(AddMessageHandler));

            var busClient = app.ApplicationServices.GetRequiredService<BusClientWrapper>();

            busClient.Subscribe<TMessage>(async (message, context) =>
            {
                var handler = app.ApplicationServices.GetRequiredService<TMessageHandler>();

                await handler.HandleMessage(message, context);
            })
            .ConfigureAwait(false);

            return app;
        }

        public static IApplicationBuilder AddRpcHandler<TRequestMessage, TResponseMessage, TRpcHandler>(this IApplicationBuilder app)
            where TRequestMessage : IRpcRequestMessage
            where TResponseMessage : IRpcResponseMessage
            where TRpcHandler : IRpcHandler<TRequestMessage, TResponseMessage>

        {
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger(nameof(AddRpcHandler));

            var busClient = app.ApplicationServices.GetRequiredService<BusClientWrapper>();

            busClient.Respond<TRequestMessage, TResponseMessage>(async (message) =>
            {
                var handler = app.ApplicationServices.GetRequiredService<TRpcHandler>();

                var response = await handler.HandleMessage(message);

                return response;
            })
            .ConfigureAwait(false);

            return app;
        }
    }
}
