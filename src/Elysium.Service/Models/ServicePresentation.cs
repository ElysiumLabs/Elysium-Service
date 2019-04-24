using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elysium.Models
{
    public class ServicePresentation : ServiceInfo
    {
        public string Branch { get; set; }

        public ServiceStatus Status { get; set; }

    }

    public static class ServicePresentationExtensions
    {
        public static ServicePresentation AsPresentation(this Service service)
        {
            var p = new ServicePresentation()
            {
                Application = service.Options.Application,
                Name = service.Options.Name,
                Version = service.Options.Version,
                Branch = service.Options.Branch,
                Status = service.Status
            };

            return p;
        }

        public static IEnumerable<ServicePresentation> AsPresentation(this IEnumerable<Service> services)
        {
            return services.Select(x => x.AsPresentation());
        }
    }
}
