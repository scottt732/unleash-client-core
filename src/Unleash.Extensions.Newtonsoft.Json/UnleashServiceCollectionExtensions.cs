using System;
using Microsoft.Extensions.DependencyInjection;
using Unleash.Serialization;

namespace Unleash
{
    public static class UnleashServiceCollectionExtensions
    {
        public static IUnleashServiceCollection WithNewtonsoftJsonSerializer(this IUnleashServiceCollection serviceCollection,
            Action<NewtonsoftJsonSerializerSettings> settingsConfigurator = null)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var settings = new NewtonsoftJsonSerializerSettings();
            settingsConfigurator?.Invoke(settings);

            serviceCollection.AddSingleton(settings);
            serviceCollection.WithJsonSerializer<NewtonsoftJsonSerializer>();

            return serviceCollection;
        }
    }
}
