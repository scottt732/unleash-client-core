using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Unleash.Internal;

namespace Unleash.Lifetime
{
    public class WebHostControlledLifetimeStartupFilter : IStartupFilter
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IUnleashServices unleashServices;
        private readonly IApplicationLifetime applicationLifetime;

        public WebHostControlledLifetimeStartupFilter(IServiceProvider serviceProvider, IUnleashServices unleashServices, IApplicationLifetime applicationLifetime)
        {
            this.serviceProvider = serviceProvider;
            this.unleashServices = unleashServices;
            this.applicationLifetime = applicationLifetime;
        }

        /// <inheritdoc />
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                var logger = serviceProvider.GetService<ILogger<WebHostControlledLifetimeStartupFilter>>();

                applicationLifetime.ApplicationStopping.Register(() =>
                {
                    logger?.LogInformation("Shutting down Unleash");
                    unleashServices.Dispose();
                });

                next(app);
            };
        }
    }
}
