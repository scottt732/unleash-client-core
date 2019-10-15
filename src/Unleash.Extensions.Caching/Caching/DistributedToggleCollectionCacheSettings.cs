using Microsoft.Extensions.Caching.Distributed;

namespace Unleash.Caching
{
    public class DistributedToggleCollectionCacheSettings
    {
        public string ToggleCollectionKeyName { get; set; }
        public string EtagKeyName { get; set; }

        public DistributedCacheEntryOptions ToggleCollectionEntryOptions { get; set; } = new DistributedCacheEntryOptions();
        public DistributedCacheEntryOptions EtagEntryOptions { get; set; } = new DistributedCacheEntryOptions();
    }
}
