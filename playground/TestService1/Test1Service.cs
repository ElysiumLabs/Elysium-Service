using System;
using System.Collections.Generic;
using System.Text;
using Elysium;
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
            services.AddMediatR();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/test1/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
