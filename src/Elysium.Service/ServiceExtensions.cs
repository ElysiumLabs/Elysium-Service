using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Elysium
{

    public static class ServiceExtensions
    {

        public static IServiceCollection AddElysiumService<TService>(this IServiceCollection services, Service parent) where TService : Service
        {
            services.AddScoped((serviceProvider) =>
            {
                var service = ActivatorUtilities.CreateInstance<TService>(serviceProvider);

                service.ConfigureParent(parent);

                return service;
            });

            services.ConfigureAddInService<TService>();

            return services;
        }

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, Action<ServiceOptions> configureOptions) where TService : Service
        {

            var service = app.ApplicationServices.GetRequiredService<TService>();

            configureOptions?.Invoke(service.Options);

            service.Options.Validate();

            return app.ConfigureUseServiceFromHost(service);
        }

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, string branch = null) where TService : Service
        {
            return app.UseElysiumService<TService>((opt) =>
            {
                opt.Branch = branch;
            });
        }


    }


}