using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elysium.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static T GetServiceFromCollection<T>(this IServiceCollection services)
        {
            return (T)services
                .LastOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        internal static ApplicationPartManager GetApplicationPartManager(this IServiceCollection services)
        {
            var manager = GetServiceFromCollection<ApplicationPartManager>(services);
            if (manager == null)
            {
                manager = new ApplicationPartManager();

                var environment = GetServiceFromCollection<IHostingEnvironment>(services);
                var entryAssemblyName = environment?.ApplicationName;
                if (string.IsNullOrEmpty(entryAssemblyName))
                {
                    return manager;
                }

                //manager.PopulateDefaultParts(entryAssemblyName);
            }

            return manager;
        }

        internal static void TryRemoveServiceAppPartsInHost<T>(this IServiceCollection services)
        {
            var partsManager = GetApplicationPartManager(services);

            var serviceLib = partsManager.ApplicationParts
                .FirstOrDefault(part => part.Name == typeof(T).Assembly.GetName().Name);

            if (serviceLib != null)
            {
                partsManager.ApplicationParts.Remove(serviceLib);
            }
        }



       
    }
}
