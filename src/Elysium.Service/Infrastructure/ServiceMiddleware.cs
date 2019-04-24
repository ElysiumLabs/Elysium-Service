using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elysium.Infrastructure
{
    [Obsolete("Not used anymore", true)]

    public class ServiceMiddleware
    {
        public static string ElysiumServicesChainKey = "ElysiumServicesChain";

        private readonly RequestDelegate _next;
        private readonly ILogger<ServiceMiddleware> _logger;
        private readonly Service _service;

        public ServiceMiddleware(RequestDelegate next, ILogger<ServiceMiddleware> logger, IService service)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service as Service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.TryAddServiceToChain(_service);

            _logger.LogInformation("ServiceChain: " + _service.ToString());

            HttpRequest request = context.Request;
            return _next(context);
        }

    
    }

    [Obsolete("Not used anymore", true)]
    public static class ServiceMiddlewareExternsions
    {
        [Obsolete("Not used anymore", true)]
        public static IEnumerable<Service> GetServiceChain(this HttpContext httpContext)
        {
            var servicesChain = httpContext.Items[ServiceMiddleware.ElysiumServicesChainKey] as List<Service>;

            if (servicesChain == null)
            {
                servicesChain = new List<Service>();
            }

            return servicesChain;

        }

        [Obsolete("Not used anymore", true)]
        internal static void TryAddServiceToChain<TService>(this HttpContext httpContext, TService service) where TService : Service
        {
            try
            {

                var servicesChain = httpContext.GetServiceChain().ToList();

                servicesChain.Add(service);

                httpContext.Items[ServiceMiddleware.ElysiumServicesChainKey] = servicesChain;
            }
            catch (Exception)
            {
                throw;
            }
          
        }
    }
}
