using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
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

            serviceCollection.AddTransient<IStartupFilter, SynchronousFlagLoadingStartupFilter>();
            return serviceCollection;
        }

        public static IUnleashServiceCollection WithWebHostControlledLifetime(this IUnleashServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            serviceCollection.AddTransient<IStartupFilter, WebHostControlledLifetimeStartupFilter>();
            return serviceCollection;
        }
    }
}
