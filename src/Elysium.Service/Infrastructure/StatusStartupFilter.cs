using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Elysium.Extensions;

namespace Elysium.Infrastructure
{
    public class ServiceStatusStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                var service = builder.ApplicationServices.GetElysiumHostService<Service>();

                if (service == null)
                {
                    throw new Exception("Host service could not be located in services");
                }

                try
                {
                    service.Status.Report(ServiceState.Starting, "Starting service - " + DateTimeOffset.UtcNow.ToString());

                    next(builder);

                    service.Status.Report(ServiceState.Started, "Service started - " + DateTimeOffset.UtcNow.ToString());
                }
                catch (Exception ex)
                {
                    service.Status.Report(ServiceState.Error, "Error starting service - " + ex.Message + DateTimeOffset.UtcNow.ToString());

                    if (service.Options.ThrowExceptionOnFailure)
                    {
                        throw;
                    }
                }

            };
        }
    }
}
