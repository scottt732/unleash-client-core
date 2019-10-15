using System.Text.Json;

namespace Unleash
{
    public class SystemTextJsonSerializerSettings
    {
        public JsonSerializerOptions SerializerOptions { get; set; }
        public JsonWriterOptions JsonWriterOptions { get; set; }
    }
}