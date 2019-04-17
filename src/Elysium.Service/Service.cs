using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public IServiceProvider ServiceProvider { get; internal protected set; }


        //
        [Obsolete("Use options instead", true)]
        public string Name { get; set; }

        [Obsolete("Use options instead", true)]
        public string Application { get; set; }

        [Obsolete("Use options instead", true)]
        public string Version { get; set; }

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

            ServiceProvider = services.BuildServiceProvider();

            return ServiceProvider;
        }

        //
        //
        //





    }

    public class ServiceOptionsOld
    {
        internal static ServiceOptionsOld GetDefaultServiceOptions()
        {
            var opt = new ServiceOptionsOld()
            {
            };
            return opt;
        }

        public ServiceOptionsOld()
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

    public class ServiceOptions : Dictionary<object, object>
    {


        public string Name
        {
            get { return this[nameof(Name)] as string; }
            set { this[nameof(Name)] = value; }
        }

        public string Application
        {
            get { return this[nameof(Application)] as string; }
            set { this[nameof(Application)] = value; }
        }

        public string Branch
        {
            get { return this[nameof(Branch)] as string; }
            set { this[nameof(Branch)] = value; }
        }

        public string Version
        {
            get { return this[nameof(Version)] as string; }
            set { this[nameof(Version)] = value; }
        }

        public static ServiceOptions CreateDefault(Service service)
        {
            var type = service.GetType();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(type.Assembly.Location);

            var opts =  new ServiceOptions()
            {
                Name = type.Name,
                Version = fvi.FileVersion
            };

            opts.Branch = opts.Name;

            if (opts.Name.EndsWith("Service", StringComparison.InvariantCultureIgnoreCase))
            {
                opts.Branch = opts.Branch.Replace("Service", "");
            }

            return opts;

        }

        public void Validate()
        {
            
        }
    }


}