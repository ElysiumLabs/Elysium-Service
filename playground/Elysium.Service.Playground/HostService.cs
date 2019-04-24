using Elysium;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using TestService1;
using TestService2;
using TestService3;
//using TestService2;

namespace Host.Playground
{
    //This is like any other Elysium service...

    public class HostService : Elysium.Service
    {
        public HostService()
        {
            Options.Name = "Host";
            Options.Version = "1.0.1";
            Options.Application = "Playground";
        }

        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            services
                .AddElysiumService<Test1Service>()
            //    //.AddElysiumService<Test2Service>()
                ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Host API", Version = "v1" });
            });
        }

        public override void Configure(IApplicationBuilder app)
        {

            app
                .UseElysiumService<Test1Service>("Test1")
                //.UseElysiumService<Test2Service>("Test2")
                ;

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Host API V1");
                //c.SwaggerEndpoint("/Test1Service/swagger/v1/swagger.json", "Host API V1"); You can see other swaggers in host swagger
            });

            app.UseMvcWithDefaultRoute();

        }

        
    }

   
}