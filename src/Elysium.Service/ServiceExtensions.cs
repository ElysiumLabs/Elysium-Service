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
            services.AddSingleton<TService>();

            services.ConfigureAddInService<TService>();

            return services;
        }
       
        public static IApplicationBuilder UseElysiumService<TService>(this IApplicationBuilder app, Action<ServiceOptions> configureOptions) where TService : Service
        {
            var service = app.ApplicationServices.GetRequiredService<TService>();

            var options = ServiceOptions.GetDefaultServiceOptions();

            configureOptions?.Invoke(options);

            options.Validate();

            return app.ConfigureUseServiceFromHost(service, options.Branch, options.AdditionalServicesConfiguration, options.AdditionalAppBuilderConfiguration);
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