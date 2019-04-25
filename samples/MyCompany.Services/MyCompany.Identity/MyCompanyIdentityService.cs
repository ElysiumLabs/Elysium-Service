using MyCompany.Identity.Services;
using Elysium;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCompany.Identity
{
    public class MyCompanyIdentityService : Service
    {
        public MyCompanyIdentityService(IHostingEnvironment environment, IConfiguration configuration, ILogger<Service> logger = null) : base(environment, configuration, logger)
        {
            Options.Name = "Identity Service TOP";
        }


        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<IdentityServiceQualquer>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = Options.Name, Version = "v1" });
            });
        }

        public override void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/"+this.Options.Branch +"/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
