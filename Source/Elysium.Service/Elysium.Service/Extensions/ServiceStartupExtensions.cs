using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApiContrib.Core;

namespace Elysium.Service
{
    public static class ServiceStartupExtensions
    {
        private static IApplicationBuilder BranchAppService(this IServiceStartup serviceStartup, IApplicationBuilder rootApp, string branch, Action<IServiceCollection> servicesConfiguration = null, Action<IApplicationBuilder> appBuilderConfiguration = null)
        {
            var serviceapp = rootApp.UseBranchWithServices(branch,
               services =>
               {
                   serviceStartup.ConfigureServices(services);
                   servicesConfiguration?.Invoke(services);
               },
               appBuilder =>
               {
                   serviceStartup.Configure(appBuilder);
                   appBuilderConfiguration?.Invoke(appBuilder);
               });

            return serviceapp;
        }

        private static void RemoveElysiumServicesApplicationParts<T>(this IMvcBuilder mvcBuilder) where T : Service
        {
            if (mvcBuilder != null)
            {
                mvcBuilder.ConfigureApplicationPartManager(apm =>
                {
                    var formsLibrary = apm.ApplicationParts.Where(x => x.Name == typeof(T).Assembly.GetName().Name).FirstOrDefault();

                    if (formsLibrary != null)
                    {
                        apm.ApplicationParts.Remove(formsLibrary);
                    }
                });
            }
        }

        public static IApplicationBuilder UseElysiumService<T>(this IApplicationBuilder app, Action<ElysiumServiceOptions> configureOptions) where T : Service
        {
            var service = app.ApplicationServices.GetRequiredService<T>();

            var options = service.GetDefaultServiceOptions();

            configureOptions?.Invoke(options);

            options.Validate();

            return service.BranchAppService(app, options.CustomBranch, options.AdditionalServicesConfiguration, options.AdditionalAppBuilderConfiguration);
        }

        public static IApplicationBuilder UseElysiumService<T>(this IApplicationBuilder app, string branch = null) where T : Service
        {
            return app.UseElysiumService<T>((opt) =>
            {
                opt.CustomBranch = branch;
            });
        }

        public static IMvcBuilder AddElysiumService<T>(this IMvcBuilder mvcBuilder) where T : Service
        {
            mvcBuilder.Services.AddSingleton<T>();

            mvcBuilder.RemoveElysiumServicesApplicationParts<T>();

            return mvcBuilder;
        }
    }
}