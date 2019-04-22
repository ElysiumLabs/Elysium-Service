using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Elysium.Infrastructure
{
    internal static class ServiceHostInitializer
    {
        internal static IServiceCollection ConfigureAddInService<TService>(this IServiceCollection services) where TService : Service
        {
            services.TryRemoveServiceAppPartsInHost<TService>();

            return services;
        }


        internal static IApplicationBuilder ConfigureUseServiceFromHost(
           this IApplicationBuilder rootApp,
           Service service
           //Action<IServiceCollection> hostServicesConfiguration = null,
           //Action<IApplicationBuilder> hostAppBuilderConfiguration = null
           )
        {
            var serviceapp = ServiceHostBuilder.BuildServiceInBranch(
                service,
                rootApp,
                StringExtensions.SlashBranchName(service.InternalOptions.Branch),
                services =>
                {
                    service.ConfigureServicesInternal(services);
                    //hostServicesConfiguration?.Invoke(services);
                },
                appBuilder =>
                {
                    service.ConfigureInternal(appBuilder);
                    //hostAppBuilderConfiguration?.Invoke(appBuilder);
                });

            return serviceapp;
        }

        
    }

    
}
