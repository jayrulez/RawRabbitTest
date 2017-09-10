using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Abstractions.Messaging
{
    public interface IRpcHandler<TRequestMessage, TResponseMessage> where TRequestMessage : IRpcRequestMessage where TResponseMessage : IRpcResponseMessage
    {
        Task<TResponseMessage> HandleMessage(TRequestMessage message);
    }
}
