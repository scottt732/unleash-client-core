using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Unleash.Internal;
using Unleash.Serialization;

namespace Unleash.Caching
{
    public class DistributedToggleCollectionCache : IToggleCollectionCache
    {
        private readonly DistributedToggleCollectionCacheSettings settings;
        private readonly IDistributedCache distributedCache;
        private readonly IJsonSerializer jsonSerializer;

        public DistributedToggleCollectionCache(DistributedToggleCollectionCacheSettings settings,
            IDistributedCache distributedCache, IJsonSerializer jsonSerializer)
        {
            this.settings = settings;
            this.distributedCache = distributedCache;
            this.jsonSerializer = jsonSerializer;
        }

        /// <inheritdoc />
        public async Task Save(ToggleCollection toggleCollection, string etag, CancellationToken cancellationToken)
        {
            using (var ms = new MemoryStream())
            {
                jsonSerializer.Serialize(ms, toggleCollection);
                ms.Seek(0, SeekOrigin.Begin);

                await distributedCache.SetAsync(settings.ToggleCollectionKeyName, ms.ToArray(), settings.ToggleCollectionEntryOptions, cancellationToken).ConfigureAwait(false);
                await distributedCache.SetStringAsync(settings.EtagKeyName, etag, settings.EtagEntryOptions, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<ToggleCollectionCacheResult> Load(CancellationToken cancellationToken)
        {
            try
            {
                var togglesJsonBytes = await distributedCache.GetAsync(settings.ToggleCollectionKeyName, cancellationToken).ConfigureAwait(false);
                var etag = await distributedCache.GetStringAsync(settings.EtagKeyName, cancellationToken).ConfigureAwait(false);

                if (togglesJsonBytes == null || etag == null)
                {
                    return ToggleCollectionCacheResult.Empty;
                }

                ToggleCollection toggleCollection;
                using (var ms = new MemoryStream(togglesJsonBytes, 0, togglesJsonBytes.Length, false))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    toggleCollection = jsonSerializer.Deserialize<ToggleCollection>(ms);
                }

                return ToggleCollectionCacheResult.FromResult(toggleCollection, etag);
            }
            catch
            {
                return ToggleCollectionCacheResult.Empty;
            }
        }
    }
}
