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
            services.AddScoped<TService>();

            services.ConfigureAddInService<TService>();

            return services;
        }

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, Service hostService, Action<ServiceOptions> configureOptions) where TService : Service
        {
                var service = app.ApplicationServices.GetRequiredService<TService>();

                hostService._children.Add(service);

                configureOptions?.Invoke(service.Options);

                service.Options.Validate();

                return app.ConfigureUseServiceFromHost(service);
            
        }

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, Service hostService, string branch = null) where TService : Service
        {
            return app.UseElysiumService<TService>(hostService, (opt) =>
            {
                opt.Branch = branch;
            });
        }

       
    }

    
}