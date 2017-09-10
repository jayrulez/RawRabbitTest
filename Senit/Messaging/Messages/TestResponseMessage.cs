using Senit.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Messaging.Messages
{
    public class TestResponseMessage : IRpcResponseMessage
    {
        public string Output { get; set; }
    }
}
