using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Elysium;
using Elysium.Infrastructure;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace TestService1
{
    public class Test1Service : Elysium.Service
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
          
            services.AddIdentityServer()
                .AddInMemoryApiResources(Bla2())
                .AddInMemoryClients(Bla())
                ;

            services.AddMediatR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
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
