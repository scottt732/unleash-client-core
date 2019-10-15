using System.IO;
using System.Text;
using System.Text.Json;
using Unleash.Serialization;

namespace Unleash
{
    public class SystemTextJsonSerializer : IJsonSerializer
    {
        private readonly SystemTextJsonSerializerSettings settings;

        public SystemTextJsonSerializer(SystemTextJsonSerializerSettings settings)
        {
            this.settings = settings;
        }

        /// <inheritdoc />
        public T Deserialize<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 65536, true))
            {
                var json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T>(json, settings.SerializerOptions);
            }
        }

        /// <inheritdoc />
        public void Serialize<T>(Stream stream, T instance)
        {
            using var jsonWriter = new Utf8JsonWriter(stream, settings.JsonWriterOptions);
            JsonSerializer.Serialize(jsonWriter, instance, settings.SerializerOptions);
        }
    }
}
