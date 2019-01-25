using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using WebApiContrib.Core;

namespace Elysium
{
    internal class ServiceInitializer
    {
        internal static IApplicationBuilder ConfigureUseServiceFromHost(
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

        internal static void ConfigureAddInService(Type type, IServiceCollection services)
        {
            TryRemoveServiceAppPartsInHost(type, services);
        }

        private static void TryRemoveServiceAppPartsInHost(Type type, IServiceCollection services)
        {
            var partsManager = Elysium.Extensions.ServiceCollectionExtensions.GetApplicationPartManager(services);

            var serviceLib = partsManager.ApplicationParts
                .FirstOrDefault(part => part.Name == type.Assembly.GetName().Name);

            if (serviceLib != null)
            {
                partsManager.ApplicationParts.Remove(serviceLib);
            }
        }

    }

    

    
}