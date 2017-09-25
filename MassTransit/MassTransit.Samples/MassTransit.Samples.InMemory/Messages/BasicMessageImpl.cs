using System;

namespace MassTransit.Samples.InMemory.Messages
{
    public class BasicMessageImpl : BasicMessage
    {
        public Guid MessageId { get; set; }
        public string Payload { get; set; }
    }
}