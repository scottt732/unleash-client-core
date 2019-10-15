using System.Text;

namespace Unleash.Serialization
{
    public class NewtonsoftJsonSerializerSettings
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public int BufferSize { get; set; } = 65536;
    }
}
