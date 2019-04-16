using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
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

        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, Action<ServiceOptions> configureOptions) where TService : Service
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<TService>();

                configureOptions?.Invoke(service.Options);

                service.Options.Validate();

                return app.ConfigureUseServiceFromHost(service);
            }
            
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