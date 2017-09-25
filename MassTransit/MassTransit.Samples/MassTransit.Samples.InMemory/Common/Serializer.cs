using Newtonsoft.Json;

namespace MassTransit.Samples.InMemory.Common
{
    public class Serializer : ISerializer
    {
        public string SerializeToJsonString(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}