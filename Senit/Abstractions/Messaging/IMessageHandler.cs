using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Abstractions.Messaging
{
    public interface IMessageHandler<TMessage> where TMessage : IMessage
    {
        Task HandleMessage(TMessage message, MessageContext messageContext);
    }
}
