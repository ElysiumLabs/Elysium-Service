using System;
using System.Collections.Generic;
using System.Text;

namespace Elysium
{
    public class ServiceOptions : ServiceInfo
    {
        public string Branch { get; set; }

        public bool Discoverable { get; set; } = true;

        public bool ThrowExceptionOnFailure { get; set; } = false;

        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();


        public static ServiceOptions CreateDefault(Service service)
        {
            var type = service.GetType();

            var opts = new ServiceOptions()
            {
                Name = type.Name,
            };

            if (opts.Name.EndsWith("Service"))
            {
                opts.Branch = opts.Branch.Replace("Service", "");
            }

            opts.Branch = opts.Branch.Trim();

            return opts;

        }

        public virtual bool Validate()
        {
            return
                (
                !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Branch)
                )
                ;
        }

    }

    public class ServiceInfo
{
        public string Name { get; set; }

        public string Application { get; set; }

        public string Version { get; set; }
    }
}
