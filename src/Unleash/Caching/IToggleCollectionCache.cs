using System.Threading;
using System.Threading.Tasks;
using Unleash.Internal;

namespace Unleash.Caching
{
    public interface IToggleCollectionCache
    {
        Task<ToggleCollectionCacheResult> Load(CancellationToken cancellationToken);
        Task Save(ToggleCollection toggleCollection, string etag, CancellationToken cancellationToken);
    }

    public interface IToggleCollectionCache<out TSettings> : IToggleCollectionCache
        where TSettings : class
    {
        TSettings Settings { get; }
    }
}
