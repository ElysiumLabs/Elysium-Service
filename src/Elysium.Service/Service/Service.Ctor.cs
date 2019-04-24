using Elysium.Extensions;
using Elysium.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elysium
{
    public abstract partial class Service : ServiceBase, IService
    {
        protected IConfiguration Configuration { get; set; }
        protected IHostingEnvironment Environment { get; set; }
        protected ILogger<Service> Logger { get; }

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
    }
}
