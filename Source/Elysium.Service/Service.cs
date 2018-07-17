using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Elysium.Service
{
    public abstract class Service : IService, IServiceStartup
    {
        //
        public string Name { get; set; }

        public string Application { get; set; }

        public string Version { get; set; }

        public IConfiguration Configuration { get; set; }

        public Service(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Service()
        {
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddApplicationPart(Assembly.GetAssembly(this.GetType()))
                .AddControllersAsServices()
                ;
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
        }

        public virtual void Initialize()
        {
        }

        internal ElysiumServiceOptions GetDefaultServiceOptions()
        {
            var opt = new ElysiumServiceOptions()
            {
            };
            return opt;
        }
    }

    public class ElysiumServiceOptions
    {
        public ElysiumServiceOptions()
        {
        }

        public string CustomBranch { get; set; }

        public Action<IServiceCollection> AdditionalServicesConfiguration { get; set; }

        public Action<IApplicationBuilder> AdditionalAppBuilderConfiguration { get; set; }

        public void Validate()
        {
            if (!string.IsNullOrEmpty(CustomBranch))
            {
            }
        }
    }
}