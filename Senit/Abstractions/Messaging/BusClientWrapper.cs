using Microsoft.Extensions.Logging;
using RawRabbit;
using RawRabbit.Common;
using System;
using System.Threading.Tasks;

namespace Senit.Abstractions.Messaging
{
    public class BusClientWrapper
    {
        private readonly IBusClient _busClient;
        private readonly ILogger _logger;

        public BusClientWrapper(IBusClient busClient, ILoggerFactory loggerFactory)
        {
            _busClient = busClient;
            _logger = loggerFactory.CreateLogger<BusClientWrapper>();
        }

        public async Task Publish<TMessage>(TMessage message)
        {
            await _busClient.PublishAsync(message);
        }

        public async Task Subscribe<TMessage>(Func<TMessage, MessageContext, Task> subscribeMethod)
        {
            await _busClient.SubscribeAsync<TMessage, MessageContext>(async (message, context) =>
            {
                await subscribeMethod.Invoke(message, context);
            });
        }

        public async Task<TResponseMessage> Request<TRequestMessage, TResponseMessage>(TRequestMessage message)
        {
            try
            {
                var response = await _busClient.RequestAsync<TRequestMessage, TResponseMessage>(message);

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(ex.Message);
            }
        }

        public async Task Respond<TRequestMessage, TResponseMessage>(Func<TRequestMessage, Task<TResponseMessage>> handler)
        {
            await _busClient.RespondAsync<TRequestMessage, TResponseMessage>(async (requestMessage) =>
            {
                try
                {
                    return await handler.Invoke(requestMessage);
                }
                catch (Exception ex)
                {
                    throw new RpcException(ex.Message);
                }
            });
        }
    }
}
