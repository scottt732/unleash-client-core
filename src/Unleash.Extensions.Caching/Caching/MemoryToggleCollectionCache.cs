using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Unleash.Internal;

namespace Unleash.Caching
{
    public class MemoryToggleCollectionCache : IToggleCollectionCache
    {
        private readonly MemoryToggleCollectionCacheSettings settings;
        private readonly IMemoryCache memoryCache;

        public MemoryToggleCollectionCache(MemoryToggleCollectionCacheSettings settings, IMemoryCache memoryCache)
        {
            this.settings = settings;
            this.memoryCache = memoryCache;
        }

        /// <inheritdoc />
        public Task<ToggleCollectionCacheResult> Load(CancellationToken cancellationToken)
        {
            if (memoryCache.TryGetValue(settings.ToggleCollectionKeyName, out ToggleCollection toggles)
                && memoryCache.TryGetValue(settings.EtagKeyName, out string etag))
            {
                return Task.FromResult(ToggleCollectionCacheResult.FromResult(toggles, etag));
            }

            return Task.FromResult(ToggleCollectionCacheResult.Empty);
        }

        /// <inheritdoc />
        public Task Save(ToggleCollection toggleCollection, string etag, CancellationToken cancellationToken)
        {
            memoryCache.Set(settings.ToggleCollectionKeyName, toggleCollection, settings.ToggleCollectionEntryOptions);
            memoryCache.Set(settings.EtagKeyName, etag, settings.EtagEntryOptions);

            return Task.CompletedTask;
        }
    }
}