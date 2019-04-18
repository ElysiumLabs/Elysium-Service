using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elysium
{
    public abstract class Service : IStartup
    {

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Configure(IApplicationBuilder app);



        internal List<Service> _children = new List<Service>();

        public IEnumerable<Service> Children
        {
            get { return _children; }
            protected set { _children = value.ToList(); }
        }


        public ServiceOptions Options { get; set; }


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
            Options = ServiceOptions.CreateDefault(this);
        }

        internal void ConfigureServicesInternal(IServiceCollection services)
        {
            ConfigureServices(services);

            ConfigureServiceParts(services);
        }

        private void ConfigureServiceParts(IServiceCollection services)
        {
            var partsManager = services.GetApplicationPartManager();

            AddAppPartsUntilElysiumService(services, partsManager);

        }

        private void AddAppPartsUntilElysiumService(IServiceCollection services, ApplicationPartManager partsManager)
        {

            var needAddAppPart = true;

            var type = this.GetType();

            if (!typeof(Service).IsAssignableFrom(type))
            {
                return;
            }

            do
            {

                var appFac = ApplicationPartFactory.GetApplicationPartFactory(type.Assembly);
                var parts = appFac.GetApplicationParts(type.Assembly);

                foreach (var part in parts)
                {
                    partsManager.ApplicationParts.Add(part);
                }

                type = type.BaseType;

                needAddAppPart = type != typeof(Service);

            }
            while (needAddAppPart);
        }

        internal void ConfigureInternal(IApplicationBuilder app)
        {
            Configure(app);
        }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            ConfigureServices(services);

            return services.BuildServiceProvider();
        }

        //
        //
        //





    }


}