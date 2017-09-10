using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Abstractions.Messaging
{
    public class RpcException : Exception
    {
        public RpcException(string message, Exception innerException = null) : base(message, innerException)
        {

        }
    }
}
