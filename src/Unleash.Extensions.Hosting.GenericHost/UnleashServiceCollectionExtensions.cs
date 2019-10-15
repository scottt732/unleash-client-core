using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unleash.Lifetime;

namespace Unleash
{
    public static class UnleashServiceCollectionExtensions
    {
        public static IUnleashServiceCollection WithSynchronousFlagLoadingOnStartup(this IUnleashServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            serviceCollection.AddSingleton<IHostedService, SynchronousFlagLoadingService>();

            return serviceCollection;
        }

        public static IUnleashServiceCollection WithHostControlledLifetime(this IUnleashServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            serviceCollection.AddSingleton<IHostedService, HostControlledLifetimeService>();

            return serviceCollection;
        }
    }
}
