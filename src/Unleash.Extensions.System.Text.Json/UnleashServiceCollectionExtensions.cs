using System;
using Microsoft.Extensions.DependencyInjection;
using Unleash.Serialization;

namespace Unleash
{
    public static class UnleashServiceCollectionExtensions
    {
        public static IUnleashServiceCollection WithSystemTextJsonSerializer(this IUnleashServiceCollection serviceCollection,
            Action<SystemTextJsonSerializerSettings> settingsConfigurator = null)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var settings = new SystemTextJsonSerializerSettings();
            settingsConfigurator?.Invoke(settings);

            serviceCollection.AddSingleton(settings);
            serviceCollection.WithJsonSerializer<SystemTextJsonSerializer>();

            return serviceCollection;
        }
    }
}
