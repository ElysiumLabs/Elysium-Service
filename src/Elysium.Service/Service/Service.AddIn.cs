using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elysium
{
    public abstract partial class Service
    {
        internal void ConfigureServicesForAddIn(IServiceCollection services)
        {
            ConfigureServicesInternal(services);
            ConfigureApplicationPartForAddInInternal(services);
        }

        private void ConfigureApplicationPartForAddInInternal(IServiceCollection services)
        {
            var partManager = services.GetApplicationPartManager();
            services.TryAddSingleton(partManager);

            ConfigureApplicationPartForAddIn(services, partManager);
        }

        protected virtual void ConfigureApplicationPartForAddIn(IServiceCollection services, ApplicationPartManager partManager)
        {
            ApplicationPartExtensions.AddApplicationPartsFromObjectAssembly(this, partManager);
        }

        internal void ConfigureForAddIn(IApplicationBuilder app)
        {
            ConfigureInternal(app);
        }

        internal void ConfigureInternal(IApplicationBuilder app)
        {
            Configure(app);
            app.UseStartPage(Options);
        }
    }
}
