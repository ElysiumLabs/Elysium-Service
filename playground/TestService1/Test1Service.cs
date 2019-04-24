using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Elysium;
using Elysium.Infrastructure;
using Elysium.StartPage;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TestService1.Utils;
using TestService2;

namespace TestService1
{
    public class Test1Service : Service
    {
        public Test1Service()
        {

        }
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddElysiumService<Test2Service>();

            services.AddIdentityServer()
                .AddInMemoryApiResources(Bla2())
                .AddInMemoryClients(Bla())
                ;

            services.AddMediatR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddSingleton<IStartPageProvider, StartPageProvider>();
        }

        private IEnumerable<ApiResource> Bla2()
        {
            return new List<ApiResource>();
        }

        private IEnumerable<Client> Bla()
        {
            return new List<Client>();
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseElysiumService<Test2Service>("Test2");


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/test1/swagger/v1/swagger.json", "My API V1");
            });

            app.UseIdentityServer();

            app.UseMvcWithDefaultRoute();
        }
    }
}
