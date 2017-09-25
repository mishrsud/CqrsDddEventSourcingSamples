using System.Threading.Tasks;
using MassTransit.Samples.InMemory.Common;
using MassTransit.Samples.InMemory.Messages;

namespace MassTransit.Samples.InMemory.Consumers
{
    public class BasicMessageConsumer : IConsumer<BasicMessage>
    {
        private readonly ILog _logger;
        private readonly ISerializer _serializer;

        public BasicMessageConsumer(ILog logger, ISerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
        }
        
        public async Task Consume(ConsumeContext<BasicMessage> context)
        {
            var message = context.Message;
            var json = _serializer.SerializeToJsonString(message);
            _logger.WriteMessage(json);
            await Task.Delay(1000);
        }
    }
}