using System;
using MassTransit.Samples.InMemory.Common;
using MassTransit.Samples.InMemory.Consumers;
using MassTransit.Samples.InMemory.Messages;

namespace MassTransit.Samples.InMemory
{
    internal class Program
    {
        private static ILog _logger;
        private static ISerializer _serializer;

        public static void Main(string[] args)
        {
            WireUpDependencies();
            var busControl = CreateAndInitializeBus();
            
            var message = new BasicMessageImpl
            {
                MessageId = Guid.NewGuid(),
                Payload = "Sample payload"
            };

            busControl.Publish(message).Wait();
            _logger.WriteMessage("Done publishing");
            Console.ReadLine();
        }

        private static void WireUpDependencies()
        {
            _logger = new Log();
            _serializer = new Serializer();
        }
        
        private static IBusControl CreateAndInitializeBus()
        {
            var busControl = Bus.Factory.CreateUsingInMemory(inMemoryBusConfigurator =>
            {
                var basicConsumer = new BasicMessageConsumer(_logger, _serializer);
                inMemoryBusConfigurator.ReceiveEndpoint("basic_Message_quue", configurator =>
                    configurator.Consumer(() => basicConsumer));
            });

            busControl.Start();
            return busControl;
        }
        
    }
}