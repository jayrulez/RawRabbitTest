using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Abstractions.Messaging
{
    public class MessageContext
    {
        public string Source { get; set; }
        public string ExecutionId { get; set; }
    }
}
