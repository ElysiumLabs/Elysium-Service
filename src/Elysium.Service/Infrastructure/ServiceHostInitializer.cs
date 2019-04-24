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
                StringExtensions.SlashBranchName(service.Options.Branch),
                services =>
                {
                    service.ConfigureServicesForAddIn(services);
                    //hostServicesConfiguration?.Invoke(services);
                },
                appBuilder =>
                {
                    service.ConfigureForAddIn(appBuilder);
                    //hostAppBuilderConfiguration?.Invoke(appBuilder);
                });

            return serviceapp;
        }

        
    }

    
}
