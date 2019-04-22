using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elysium
{
    public abstract class Service : IStartup, IDisposable
    {

        public abstract void ConfigureServices(IServiceCollection services);

        public abstract void Configure(IApplicationBuilder app);


        internal void ConfigureParent(Service parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Parent._children.Add(this);
        }

        internal List<Service> _children = new List<Service>();

        public IEnumerable<Service> Children
        {
            get { return _children; }
            protected set { _children = value.ToList(); }
        }


        public ServiceOptions Options { get; set; }


        public IConfiguration Configuration { get; set; }
        public IHostingEnvironment Environment { get; set; }
        public ILogger<Service> Logger { get; }

        public Service Parent { get; private set; }

        public ServiceStatus Status { get; set; } = ServiceStatus.NotStarted;


        public Service(IHostingEnvironment environment) : this(environment, null)
        {
        }

        public Service(IConfiguration configuration) : this(null, configuration)
        {
        }

        public Service(IHostingEnvironment environment, IConfiguration configuration, ILogger<Service> logger = null) : this()
        {
            Environment = environment;
            Configuration = configuration;
            Logger = logger;
        }

        public Service()
        {
            Options = ServiceOptions.CreateDefault(this);
        }

        internal void ConfigureServicesInternal(IServiceCollection services)
        {
            try
            {
                ConfigureServices(services);

                ConfigureServiceParts(services);

            }
            catch (Exception)
            {
                Status = ServiceStatus.Error;
                //throw;
            }

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
            Status = ServiceStatus.Starting;

            try
            {
                Configure(app);

                Status = ServiceStatus.Started;
            }
            catch (Exception)
            {
                Status = ServiceStatus.Error;
                //throw;
            }

            
        }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            ConfigureServices(services);

            return services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (Parent != null)
            {
                if (Parent.Children.Contains(this))
                {
                    Parent._children.Remove(this);
                }
            }
        }

        //
        //
        //





    }

    public enum ServiceStatus
    {
        NotStarted, 
        Starting,
        Started,
        Error
    }


}