using System;

namespace MassTransit.Samples.InMemory.Common
{
    public class Log : ILog
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}