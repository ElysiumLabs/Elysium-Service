using MediatR;
using Microsoft.AspNetCore.Builder;
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
            services.AddMvc();

            services.AddMediatR();
        }

        public override void Configure(IApplicationBuilder app)
        {
            throw new Exception("test");
            app.UseMvcWithDefaultRoute();
        }
    }
}
