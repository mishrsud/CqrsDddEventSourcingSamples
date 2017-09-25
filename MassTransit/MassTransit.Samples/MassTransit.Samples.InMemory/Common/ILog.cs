namespace MassTransit.Samples.InMemory.Common
{
    public interface ILog
    {
        void WriteMessage(string message);
    }
}