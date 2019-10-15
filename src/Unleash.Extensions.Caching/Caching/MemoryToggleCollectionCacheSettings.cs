using Microsoft.Extensions.Caching.Memory;

namespace Unleash.Caching
{
    public class MemoryToggleCollectionCacheSettings
    {
        public string EtagKeyName { get; set; }
        public string ToggleCollectionKeyName { get; set; }
        public MemoryCacheEntryOptions EtagEntryOptions { get; set; } = new MemoryCacheEntryOptions();
        public MemoryCacheEntryOptions ToggleCollectionEntryOptions { get; set; } = new MemoryCacheEntryOptions();
    }
}