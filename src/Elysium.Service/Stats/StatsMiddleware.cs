using Elysium.Extensions;
using Elysium.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elysium.Stats
{
    public class StatsMiddleware
    {
        private RequestDelegate _next;
        private readonly Service _service;

        public StatsMiddleware(RequestDelegate next, IService service)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _service = service as Service ?? throw new ArgumentNullException(nameof(service));
        }


        public async Task Invoke(HttpContext context)
        {
            if (!(context.Request.Path == "/stats"))
            {
                // jump to the next middleware
                await _next.Invoke(context);
                return;
            }

            var serviceChildren = _service.Children.AsPresentation();

            var serviceStatus = new ObjectResult(serviceChildren)
            {
                DeclaredType = typeof(ServicePresentation)
            };

            if (serviceChildren.Any(x => x.Status.State != ServiceState.Started))
            {
                serviceStatus.StatusCode = 206;
            }
            else
            {
                serviceStatus.StatusCode = 200;
            }

            await context.ExecuteResultAsync(serviceStatus);

            // do your stuff
        }
    }
}
