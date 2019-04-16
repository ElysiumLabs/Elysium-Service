using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elysium.Infrastructure
{
    public class ServiceMiddleware
    {
        public static string ElysiumServicesChainKey = "ElysiumServicesChain";

        private readonly RequestDelegate _next;
        private readonly ILogger<ServiceMiddleware> _logger;
        private readonly Service _service;

        public ServiceMiddleware(RequestDelegate next, ILogger<ServiceMiddleware> logger, Service service)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.TryAddServiceToChain(_service);

            _logger.LogInformation("ServiceChain: " + _service.ToString());

            HttpRequest request = context.Request;
            return _next(context);
        }

    
    }

    public static class ServiceMiddlewareExternsions
    {
        public static IEnumerable<Service> GetServiceChain(this HttpContext httpContext)
        {
            var servicesChain = httpContext.Items[ServiceMiddleware.ElysiumServicesChainKey] as List<Service>;

            if (servicesChain == null)
            {
                servicesChain = new List<Service>();
            }

            return servicesChain;

        }


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
