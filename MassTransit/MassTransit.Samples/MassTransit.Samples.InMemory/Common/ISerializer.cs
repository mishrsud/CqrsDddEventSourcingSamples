namespace MassTransit.Samples.InMemory.Common
{
    public interface ISerializer
    {
        string SerializeToJsonString(object value);
    }
}