using Elysium;
using Microsoft.AspNetCore.Builder;
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
            Name = "Host";
            Version = "1.0.1";
            Application = "Playground";
        }

        public override void ConfigureServices(IServiceCollection services)
        {

            services
                .AddElysiumService<Test1Service>()
                .AddElysiumService<Test2Service>()
                .AddElysiumService<Test3Service>() //this has TestService2 inside
                ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Host API", Version = "v1" });
            });
        }

        public override void Configure(IApplicationBuilder app)
        {

            app
                .UseElysiumService<Test1Service>("Test1Service")
                .UseElysiumService<Test2Service>("Test2Service")
                .UseElysiumService<Test1Service>("OtherTest1Service")
                .UseElysiumService<Test3Service>("Test3Service")
                ;

            //app.UseWelcomePage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Host API V1");
                //c.SwaggerEndpoint("/Test1Service/swagger/v1/swagger.json", "Host API V1"); You can see other swaggers in host swagger
            });
        }
    }

   
}