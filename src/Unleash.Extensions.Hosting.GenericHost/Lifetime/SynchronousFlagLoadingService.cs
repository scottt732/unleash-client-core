using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Unleash.Internal;

namespace Unleash.Lifetime
{
    public class SynchronousFlagLoadingService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IUnleashServices unleashServices;

        public SynchronousFlagLoadingService(IServiceProvider serviceProvider, IUnleashServices unleashServices)
        {
            this.serviceProvider = serviceProvider;
            this.unleashServices = unleashServices;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var logger = serviceProvider.GetService<ILogger<SynchronousFlagLoadingService>>();

            logger?.LogInformation("Waiting for Unleash to load feature flags");
            await unleashServices.InitializationTask;
            logger?.LogInformation("Flags loaded.");
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
