using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestService2
{
    public class Test2Service : Elysium.Service
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddMediatR();
        }
    }
}
