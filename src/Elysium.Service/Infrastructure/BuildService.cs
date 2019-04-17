using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elysium.Infrastructure
{
    public class BuildService
    {
        public BuildService(Service service, IServiceProvider applicationServices)
        {
            Service = service;
            ApplicationServices = applicationServices;
        }

        public Service Service { get; protected set; }

        public IServiceProvider ApplicationServices { get; protected set; }
    }
}
