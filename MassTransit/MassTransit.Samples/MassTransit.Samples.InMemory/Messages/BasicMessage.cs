using System;

namespace MassTransit.Samples.InMemory.Messages
{
    public interface BasicMessage
    {
        Guid MessageId { get; set; }
        string Payload { get; set; }
    }
}