using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Elysium
{
    public abstract class Service 
    {

        //
        public string Name { get; set; }

        public string Application { get; set; }

        public string Version { get; set; }


        public IConfiguration Configuration { get; set; }

        public IHostingEnvironment Environment { get; set; }



        public Service(IHostingEnvironment environment) : this(environment, null)
        {
        }

        public Service(IConfiguration configuration) : this(null, configuration)
        {
        }

        public Service(IHostingEnvironment environment, IConfiguration configuration) : this()
        {
            Environment = environment;
            Configuration = configuration;
        }

        public Service()
        {
        }
        
        internal void ConfigureServicesInternal(IServiceCollection services)
        {
            ConfigureMvc(services.AddMvc());
            ConfigureServices(services);
        }

        public abstract void ConfigureServices(IServiceCollection services);

        public virtual IMvcBuilder ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddApplicationPart(Assembly.GetAssembly(this.GetType()))
                .AddControllersAsServices()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Clear();
                    manager.FeatureProviders.Add(new UnderServiceNamespaceControllerFeatureProvider(this));
                });
            return mvcBuilder;
        }

        internal void ConfigureInternal(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
            Configure(app);
        }

        public abstract void Configure(IApplicationBuilder app);


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

        public string Branch { get; set; }

        public Action<IServiceCollection> AdditionalServicesConfiguration { get; set; }

        public Action<IApplicationBuilder> AdditionalAppBuilderConfiguration { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Branch))
            {
                throw new ArgumentNullException("Branch is empty");
            }


        }
    }
}