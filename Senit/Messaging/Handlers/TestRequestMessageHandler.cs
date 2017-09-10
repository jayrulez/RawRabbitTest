using Senit.Abstractions.Messaging;
using Senit.Messaging.Messages;
using System.Threading.Tasks;

namespace Senit.Messaging.Handlers
{
    public class TestRequestMessageHandler : IRpcHandler<TestRequestMessage, TestResponseMessage>
    {
        public Task<TestResponseMessage> HandleMessage(TestRequestMessage message)
        {
            return Task.FromResult(new TestResponseMessage
            {
                Output = "out"
            });
        }
    }
}
