using Senit.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senit.Messaging.Messages
{
    public class TestRequestMessage2 : IRpcRequestMessage
    {
        public string Input { get; set; }
    }
}
