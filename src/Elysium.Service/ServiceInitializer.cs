using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebApiContrib.Core;

namespace Elysium
{
    internal class ServiceInitializer
    {
        internal static IApplicationBuilder ConfigureServiceFromRoot(
            IApplicationBuilder rootApp,
            Service service,
            string branch,
            Action<IServiceCollection> hostServicesConfiguration = null,
            Action<IApplicationBuilder> hostAppBuilderConfiguration = null)
        {

            branch = CleanBranchName(branch);

            var serviceapp = rootApp.UseBranchWithServices(branch,
               services =>
               {
                   service.ConfigureServicesInternal(services);
                   hostServicesConfiguration?.Invoke(services);
               },
               appBuilder =>
               {
                   service.ConfigureInternal(appBuilder);
                   hostAppBuilderConfiguration?.Invoke(appBuilder);
               });

            return serviceapp;
        }

        private static string CleanBranchName(string branch)
        {
            return branch.StartsWith("/") ? branch : "/" + branch;
        }
    }

    
}