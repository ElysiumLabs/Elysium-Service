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
    public abstract partial class Service
    {
        public Service Parent { get; private set; }


        internal List<Service> _children = new List<Service>();

        public IEnumerable<Service> Children
        {
            get { return _children; }
            protected set { _children = value.ToList(); }
        }

        internal void ConfigureParent(Service parent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Parent._children.Add(this);
        }

    }
}
