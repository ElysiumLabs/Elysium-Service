using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using WebApiContrib.Core;

namespace Elysium
{

    public static class ServiceExtensions
    {

        public static IServiceCollection AddElysiumService<T>(this IServiceCollection services) where T : Service
        {
            services.AddSingleton<T>();

            ServiceInitializer.ConfigureAddInService(typeof(T), services);

            return services;
        }
       
        public static IApplicationBuilder UseElysiumService<T>(this IApplicationBuilder app, Action<ElysiumServiceOptions> configureOptions) where T : Service
        {
            var service = app.ApplicationServices.GetRequiredService<T>();

            var options = service.GetDefaultServiceOptions();

            configureOptions?.Invoke(options);

            options.Validate();

            return ServiceInitializer.ConfigureUseServiceFromHost(app, service, options.Branch, options.AdditionalServicesConfiguration, options.AdditionalAppBuilderConfiguration);
        }

        public static IApplicationBuilder UseElysiumService<T>(this IApplicationBuilder app, string branch = null) where T : Service
        {
            return app.UseElysiumService<T>((opt) =>
            {
                opt.Branch = branch;
            });
        }

       
    }

    
}