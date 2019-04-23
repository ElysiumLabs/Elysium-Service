using Elysium.Extensions;
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

        public static IServiceCollection AddElysiumService<TService>(this IServiceCollection services) where TService : Service
        {
            services.AddScoped((serviceProvider) =>
            {
                var hostService = serviceProvider.GetElysiumHostService();
                var service = ActivatorUtilities.CreateInstance<TService>(serviceProvider);
                service.ConfigureParent(hostService); 
                return service;
            });

            services.RemoveServiceAppPartsInHost<TService>();

            return services;
        }

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, Action<ServiceOptions> configureOptions) where TService : Service
        {

            var service = app.ApplicationServices.GetRequiredService<TService>();

            configureOptions?.Invoke(service.Options);

            service.Options.Validate();

            try
            {
                return app.ConfigureUseServiceFromHost(service);
            }
            catch (Exception)
            {
                //Empty for StatusStartupFilterManagement
            }

            return app;
        }

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, string branch = null) where TService : Service
        {
            return app.UseElysiumService<TService>((opt) =>
            {
                if (!string.IsNullOrEmpty(branch))
                {
                    opt.Branch = branch;
                }
            });
        }


    }


}