using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Unleash.Internal;

namespace Unleash.Lifetime
{
    public class SynchronousFlagLoadingStartupFilter : IStartupFilter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IUnleashServices unleashServices;

        public SynchronousFlagLoadingStartupFilter(IServiceProvider serviceProvider, IUnleashServices unleashServices)
        {
            this.serviceProvider = serviceProvider;
            this.unleashServices = unleashServices;
        }

        /// <inheritdoc />
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            var logger = serviceProvider.GetService<ILogger<SynchronousFlagLoadingStartupFilter>>();

            logger?.LogInformation("Waiting for Unleash to load feature flags");
            unleashServices.InitializationTask.GetAwaiter().GetResult();
            logger?.LogInformation("Flags loaded.");

            return next;
        }
    }
}
