using System;
using Microsoft.Extensions.DependencyInjection;

namespace Unleash
{
    using Caching;

    public static class UnleashServiceCollectionExtensions
    {
        public static IUnleashServiceCollection WithDistributedToggleCollectionCache(this IUnleashServiceCollection serviceCollection, Action<DistributedToggleCollectionCacheSettings> settingsConfigurator = null)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var settings = new DistributedToggleCollectionCacheSettings();
            settingsConfigurator?.Invoke(settings);

            if (string.IsNullOrEmpty(settings.ToggleCollectionKeyName))
            {
                throw new ArgumentException($"The {nameof(settings.ToggleCollectionKeyName)} setting is required", nameof(settings));
            }

            if (string.IsNullOrEmpty(settings.EtagKeyName))
            {
                throw new ArgumentException($"The {nameof(settings.EtagKeyName)} setting is required", nameof(settings));
            }

            serviceCollection.AddSingleton(settings);
            serviceCollection.WithToggleCollectionCache<DistributedToggleCollectionCache>();

            return serviceCollection;
        }

        public static IUnleashServiceCollection WithMemoryToggleCollectionCache(this IUnleashServiceCollection serviceCollection, Action<MemoryToggleCollectionCacheSettings> settingsConfigurator = null)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var settings = new MemoryToggleCollectionCacheSettings();
            settingsConfigurator?.Invoke(settings);

            if (string.IsNullOrEmpty(settings.ToggleCollectionKeyName))
            {
                throw new ArgumentException($"The {nameof(settings.ToggleCollectionKeyName)} setting is required", nameof(settings));
            }

            if (string.IsNullOrEmpty(settings.EtagKeyName))
            {
                throw new ArgumentException($"The {nameof(settings.EtagKeyName)} setting is required", nameof(settings));
            }

            serviceCollection.AddSingleton(settings);
            serviceCollection.WithToggleCollectionCache<MemoryToggleCollectionCache>();

            return serviceCollection;
        }
    }
}
