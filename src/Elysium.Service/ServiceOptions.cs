using System;
using System.Diagnostics;

namespace Elysium
{
    public class ServiceOptions
    {

        public string Name { get; set; }

        public string Application { get; set; }

        public string Branch { get; set; }

        public string Version { get; set; }


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

        public virtual bool Validate()
        {
            return 
                (
                !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Application)
                )
                ;
        }
    }


}