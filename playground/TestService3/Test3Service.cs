using Elysium;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TestService2;

namespace TestService3
{
    public class Test3Service : Service
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddElysiumService<Test2Service>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseElysiumService<Test2Service>("test2");

            //app.UseMvcWithDefaultRoute();

        }
    }
}
